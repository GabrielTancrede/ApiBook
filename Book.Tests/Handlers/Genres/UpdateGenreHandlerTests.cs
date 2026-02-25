using AutoMapper;
using Book.Application.DTOs.Genre;
using Book.Application.Features.Genres.Commands;
using Book.Application.Features.Genres.Handlers;
using Book.Application.Features.Genres.Mappings;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using Book.Core.Entites;
using Book.Core.Enum;
using FluentAssertions;
using Moq;

namespace Book.Tests.Handlers.Genres
{
    public class UpdateGenreHandlerTests
    {
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly IMapper _mapper;
        private readonly UpdateGenreHandler _handler;

        public UpdateGenreHandlerTests()
        {
            _genreRepositoryMock = new Mock<IGenreRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GenreProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new UpdateGenreHandler(_genreRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_GeneroExistente_DeveRetornarSucesso()
        {
            // Arrange
            var genreId = 1;
            var existingGenre = new Genre
            {
                Id = genreId,
                Name = "Nome Antigo",
                Description = "Desc antiga",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            _genreRepositoryMock
                .Setup(r => r.GetByIdAsync(genreId))
                .ReturnsAsync(existingGenre);

            _genreRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<Genre>()))
                .Returns(Task.CompletedTask);

            _genreRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var command = new UpdateGenreCommand
            {
                Id = genreId,
                Dto = new UpdateGenreDto
                {
                    Name = "Nome Atualizado",
                    Description = "Desc atualizada"
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ResultType.Should().Be(ResultType.Success);
            result.Object.Should().NotBeNull();
            result.Object.Name.Should().Be("Nome Atualizado");

            _genreRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Genre>()), Times.Once);
            _genreRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_GeneroInexistente_DeveRetornarNotFound()
        {
            // Arrange
            _genreRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Genre?)null);

            var command = new UpdateGenreCommand
            {
                Id = 999,
                Dto = new UpdateGenreDto { Name = "Qualquer" }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);

            _genreRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Genre>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NomeDuplicado_DeveRetornarInvalid()
        {
            // Arrange
            var genreId = 1;
            var existingGenre = new Genre
            {
                Id = genreId,
                Name = "Terror",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            _genreRepositoryMock
                .Setup(r => r.GetByIdAsync(genreId))
                .ReturnsAsync(existingGenre);

            _genreRepositoryMock
                .Setup(r => r.ExistsByNameAsync("Romance", genreId))
                .ReturnsAsync(true);

            var command = new UpdateGenreCommand
            {
                Id = genreId,
                Dto = new UpdateGenreDto { Name = "Romance" }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.Invalid);
            result.Message.Should().Contain("Já existe");

            _genreRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Genre>()), Times.Never);
        }
    }
}
