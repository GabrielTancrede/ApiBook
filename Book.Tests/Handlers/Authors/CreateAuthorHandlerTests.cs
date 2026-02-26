using AutoMapper;
using Book.Application.DTOs.Author;
using Book.Application.Features.Authors.Commands;
using Book.Application.Features.Authors.Handlers;
using Book.Application.Features.Authors.Mappings;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Author;
using Book.Core.Entities;
using Book.Core.Enum;
using FluentAssertions;
using Moq;

namespace Book.Tests.Handlers.Authors
{
    public class CreateAuthorHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateAuthorHandler _handler;

        public CreateAuthorHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AuthorProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new CreateAuthorHandler(_authorRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ComDadosValidos_DeveRetornarSucesso()
        {
            // Arrange
            var command = new CreateAuthorCommand
            {
                Dto = new CreateAuthorDto
                {
                    Name = "Machado de Assis",
                    Biography = "Escritor brasileiro",
                    BirthDate = new DateTime(1839, 6, 21)
                }
            };

            _authorRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Author>()))
                .Returns(Task.CompletedTask);

            _authorRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ResultType.Should().Be(ResultType.Success);
            result.Object.Should().NotBeNull();
            result.Object.Name.Should().Be("Machado de Assis");
            result.Object.Biography.Should().Be("Escritor brasileiro");

            _authorRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Author>()), Times.Once);
            _authorRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_SemBiografia_DeveRetornarSucesso()
        {
            // Arrange
            var command = new CreateAuthorCommand
            {
                Dto = new CreateAuthorDto
                {
                    Name = "Autor Teste"
                }
            };

            _authorRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Author>()))
                .Returns(Task.CompletedTask);

            _authorRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Object.Name.Should().Be("Autor Teste");
            result.Object.Biography.Should().BeNull();
        }

        [Fact]
        public async Task Handle_NomeDuplicado_DeveRetornarInvalid()
        {
            // Arrange
            var command = new CreateAuthorCommand
            {
                Dto = new CreateAuthorDto
                {
                    Name = "Machado de Assis",
                    Biography = "Escritor"
                }
            };

            _authorRepositoryMock
                .Setup(r => r.ExistsByNameAsync("Machado de Assis", null))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.Invalid);
            result.Message.Should().Contain("Já existe");

            _authorRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Author>()), Times.Never);
        }
    }
}
