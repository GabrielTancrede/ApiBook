using AutoMapper;
using Book.Application.DTOs.Genre;
using Book.Application.Features.Genres.Commands;
using Book.Application.Features.Genres.Handlers;
using Book.Application.Features.Genres.Mappings;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using Book.Core.Entities;
using Book.Core.Enum;
using FluentAssertions;
using Moq;

namespace Book.Tests.Handlers.Genres
{
    public class CreateGenreHandlerTests
    {
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateGenreHandler _handler;

        public CreateGenreHandlerTests()
        {
            _genreRepositoryMock = new Mock<IGenreRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GenreProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new CreateGenreHandler(_genreRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ComDadosValidos_DeveRetornarSucesso()
        {
            // Arrange
            var command = new CreateGenreCommand
            {
                Dto = new CreateGenreDto
                {
                    Name = "Ficção Científica",
                    Description = "Gênero literário de ficção científica"
                }
            };

            _genreRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Genre>()))
                .Returns(Task.CompletedTask);

            _genreRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ResultType.Should().Be(ResultType.Success);
            result.Object.Should().NotBeNull();
            result.Object.Name.Should().Be("Ficção Científica");

            _genreRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Genre>()), Times.Once);
            _genreRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_SemDescricao_DeveRetornarSucesso()
        {
            // Arrange
            var command = new CreateGenreCommand
            {
                Dto = new CreateGenreDto
                {
                    Name = "Terror"
                }
            };

            _genreRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Genre>()))
                .Returns(Task.CompletedTask);

            _genreRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Object.Name.Should().Be("Terror");
            result.Object.Description.Should().BeNull();
        }

        [Fact]
        public async Task Handle_NomeDuplicado_DeveRetornarInvalid()
        {
            // Arrange
            var command = new CreateGenreCommand
            {
                Dto = new CreateGenreDto
                {
                    Name = "Ficção Científica",
                    Description = "Descrição"
                }
            };

            _genreRepositoryMock
                .Setup(r => r.ExistsByNameAsync("Ficção Científica", null))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.Invalid);
            result.Message.Should().Contain("Já existe");

            _genreRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Genre>()), Times.Never);
        }
    }
}
