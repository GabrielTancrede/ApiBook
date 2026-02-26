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
    public class UpdateAuthorHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly IMapper _mapper;
        private readonly UpdateAuthorHandler _handler;

        public UpdateAuthorHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AuthorProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new UpdateAuthorHandler(_authorRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_AutorExistente_DeveRetornarSucesso()
        {
            // Arrange
            var authorId = 1;
            var existingAuthor = new Author
            {
                Id = authorId,
                Name = "Nome Antigo",
                Biography = "Bio antiga",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            _authorRepositoryMock
                .Setup(r => r.GetByIdAsync(authorId))
                .ReturnsAsync(existingAuthor);

            _authorRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Author>()))
                .Returns(Task.CompletedTask);

            _authorRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var command = new UpdateAuthorCommand
            {
                Id = authorId,
                Dto = new UpdateAuthorDto
                {
                    Name = "Nome Atualizado",
                    Biography = "Bio atualizada"
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ResultType.Should().Be(ResultType.Success);
            result.Object.Should().NotBeNull();
            result.Object.Name.Should().Be("Nome Atualizado");

            _authorRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Author>()), Times.Once);
            _authorRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_AutorInexistente_DeveRetornarNotFound()
        {
            // Arrange
            _authorRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Author?)null);

            var command = new UpdateAuthorCommand
            {
                Id = 999,
                Dto = new UpdateAuthorDto { Name = "Qualquer" }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);

            _authorRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NomeDuplicado_DeveRetornarInvalid()
        {
            // Arrange
            var authorId = 1;
            var existingAuthor = new Author
            {
                Id = authorId,
                Name = "Autor Original",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            _authorRepositoryMock
                .Setup(r => r.GetByIdAsync(authorId))
                .ReturnsAsync(existingAuthor);

            _authorRepositoryMock
                .Setup(r => r.ExistsByNameAsync("Machado de Assis", authorId))
                .ReturnsAsync(true);

            var command = new UpdateAuthorCommand
            {
                Id = authorId,
                Dto = new UpdateAuthorDto { Name = "Machado de Assis" }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.Invalid);
            result.Message.Should().Contain("Já existe");

            _authorRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Author>()), Times.Never);
        }
    }
}
