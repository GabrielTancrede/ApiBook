using Book.Application.Features.Genres.Commands;
using Book.Application.Features.Genres.Handlers;
using Book.Application.Interfaces.Repositories;
using Book.Core.Entities;
using Book.Core.Enum;
using FluentAssertions;
using Moq;

namespace Book.Tests.Handlers.Genres
{
    public class DeleteGenreHandlerTests
    {
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly DeleteGenreHandler _handler;

        public DeleteGenreHandlerTests()
        {
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _handler = new DeleteGenreHandler(_genreRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_GeneroSemLivros_DeveRetornarSucesso()
        {
            // Arrange
            var genreId = 1;
            var genre = new Genre { Id = genreId, Name = "Gênero Teste" };

            _genreRepositoryMock
                .Setup(r => r.GetByIdAsync(genreId))
                .ReturnsAsync(genre);

            _genreRepositoryMock
                .Setup(r => r.HasBooksAsync(genreId))
                .ReturnsAsync(false);

            _genreRepositoryMock
                .Setup(r => r.RemoveAsync(It.IsAny<Genre>()))
                .Returns(Task.CompletedTask);

            _genreRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var command = new DeleteGenreCommand { Id = genreId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ResultType.Should().Be(ResultType.Success);

            _genreRepositoryMock.Verify(r => r.RemoveAsync(genre), Times.Once);
            _genreRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_GeneroComLivros_DeveRetornarInvalid()
        {
            // Arrange
            var genreId = 1;
            var genre = new Genre { Id = genreId, Name = "Gênero Com Livros" };

            _genreRepositoryMock
                .Setup(r => r.GetByIdAsync(genreId))
                .ReturnsAsync(genre);

            _genreRepositoryMock
                .Setup(r => r.HasBooksAsync(genreId))
                .ReturnsAsync(true);

            var command = new DeleteGenreCommand { Id = genreId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.Invalid);

            _genreRepositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Genre>()), Times.Never);
        }

        [Fact]
        public async Task Handle_GeneroInexistente_DeveRetornarNotFound()
        {
            // Arrange
            _genreRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Genre?)null);

            var command = new DeleteGenreCommand { Id = 999 };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);

            _genreRepositoryMock.Verify(r => r.HasBooksAsync(It.IsAny<int>()), Times.Never);
            _genreRepositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Genre>()), Times.Never);
        }
    }
}
