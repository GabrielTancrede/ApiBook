using Book.Application.Features.Books.Commands;
using Book.Application.Features.Books.Handlers;
using Book.Application.Interfaces.Repositories;
using Book.Core.Enum;
using FluentAssertions;
using Moq;
using BookEntity = Book.Core.Entites.Book;

namespace Book.Tests.Handlers.Books
{
    public class DeleteBookHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly DeleteBookHandler _handler;

        public DeleteBookHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _handler = new DeleteBookHandler(_bookRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_LivroExistente_DeveRetornarSucesso()
        {
            // Arrange
            var bookId = 1;
            var book = new BookEntity { Id = bookId, Title = "Livro Teste", AuthorId = 1, GenreId = 1 };

            _bookRepositoryMock
                .Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(book);

            _bookRepositoryMock
                .Setup(r => r.RemoveAsync(It.IsAny<BookEntity>()))
                .Returns(Task.CompletedTask);

            _bookRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var command = new DeleteBookCommand { Id = bookId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ResultType.Should().Be(ResultType.Success);

            _bookRepositoryMock.Verify(r => r.RemoveAsync(book), Times.Once);
            _bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_LivroInexistente_DeveRetornarNotFound()
        {
            // Arrange
            _bookRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((BookEntity?)null);

            var command = new DeleteBookCommand { Id = 999 };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);

            _bookRepositoryMock.Verify(r => r.RemoveAsync(It.IsAny<BookEntity>()), Times.Never);
        }
    }
}
