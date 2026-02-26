using Book.Application.Features.Authors.Commands;
using Book.Application.Features.Authors.Handlers;
using Book.Application.Interfaces.Repositories;
using Book.Core.Entities;
using Book.Core.Enum;
using FluentAssertions;
using Moq;

namespace Book.Tests.Handlers.Authors
{
    public class DeleteAuthorHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly DeleteAuthorHandler _handler;

        public DeleteAuthorHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _handler = new DeleteAuthorHandler(_authorRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_AutorSemLivros_DeveRetornarSucesso()
        {
            // Arrange
            var authorId = 1;
            var author = new Author { Id = authorId, Name = "Autor Teste" };

            _authorRepositoryMock
                .Setup(r => r.GetByIdAsync(authorId))
                .ReturnsAsync(author);

            _authorRepositoryMock
                .Setup(r => r.HasBooksAsync(authorId))
                .ReturnsAsync(false);

            _authorRepositoryMock
                .Setup(r => r.RemoveAsync(It.IsAny<Author>()))
                .Returns(Task.CompletedTask);

            _authorRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var command = new DeleteAuthorCommand { Id = authorId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ResultType.Should().Be(ResultType.Success);

            _authorRepositoryMock.Verify(r => r.RemoveAsync(author), Times.Once);
            _authorRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_AutorComLivros_DeveRetornarInvalid()
        {
            // Arrange
            var authorId = 1;
            var author = new Author { Id = authorId, Name = "Autor Com Livros" };

            _authorRepositoryMock
                .Setup(r => r.GetByIdAsync(authorId))
                .ReturnsAsync(author);

            _authorRepositoryMock
                .Setup(r => r.HasBooksAsync(authorId))
                .ReturnsAsync(true);

            var command = new DeleteAuthorCommand { Id = authorId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.Invalid);

            _authorRepositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_AutorInexistente_DeveRetornarNotFound()
        {
            // Arrange
            _authorRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Author?)null);

            var command = new DeleteAuthorCommand { Id = 999 };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);

            _authorRepositoryMock.Verify(r => r.HasBooksAsync(It.IsAny<int>()), Times.Never);
            _authorRepositoryMock.Verify(r => r.RemoveAsync(It.IsAny<Author>()), Times.Never);
        }
    }
}
