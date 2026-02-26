using AutoMapper;
using Book.Application.DTOs.Book;
using Book.Application.Features.Books.Commands;
using Book.Application.Features.Books.Handlers;
using Book.Application.Features.Books.Mappings;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Book;
using Book.Core.Entities;
using Book.Core.Enum;
using FluentAssertions;
using Moq;
using BookEntity = Book.Core.Entities.Book;

namespace Book.Tests.Handlers.Books
{
    public class UpdateBookHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly IMapper _mapper;
        private readonly UpdateBookHandler _handler;

        public UpdateBookHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _genreRepositoryMock = new Mock<IGenreRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new UpdateBookHandler(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _genreRepositoryMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_LivroExistente_DeveRetornarSucesso()
        {
            // Arrange
            var bookId = 1;
            var existingBook = new BookEntity
            {
                Id = bookId,
                Title = "Título Antigo",
                AuthorId = 1,
                GenreId = 1,
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            };

            _bookRepositoryMock
                .Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(existingBook);

            _authorRepositoryMock
                .Setup(r => r.ExistsAsync(2))
                .ReturnsAsync(true);

            _genreRepositoryMock
                .Setup(r => r.ExistsAsync(2))
                .ReturnsAsync(true);

            _bookRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<BookEntity>()))
                .Returns(Task.CompletedTask);

            _bookRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var updatedBookWithRelations = new BookEntity
            {
                Id = bookId,
                Title = "Título Novo",
                AuthorId = 2,
                GenreId = 2,
                Author = new Author { Id = 2, Name = "Novo Autor" },
                Genre = new Genre { Id = 2, Name = "Novo Gênero" }
            };

            _bookRepositoryMock
                .Setup(r => r.GetByIdWithRelationsAsync(bookId))
                .ReturnsAsync(updatedBookWithRelations);

            var command = new UpdateBookCommand
            {
                Id = bookId,
                Dto = new UpdateBookDto
                {
                    Title = "Título Novo",
                    PublicationDate = DateTime.Now,
                    AuthorId = 2,
                    GenreId = 2
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ResultType.Should().Be(ResultType.Success);
            result.Object.Should().NotBeNull();
            result.Object.Title.Should().Be("Título Novo");

            _bookRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<BookEntity>()), Times.Once);
            _bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_LivroInexistente_DeveRetornarNotFound()
        {
            // Arrange
            _bookRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((BookEntity?)null);

            var command = new UpdateBookCommand
            {
                Id = 999,
                Dto = new UpdateBookDto
                {
                    Title = "Qualquer",
                    PublicationDate = DateTime.Now,
                    AuthorId = 1,
                    GenreId = 1
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);

            _bookRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<BookEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_AutorInexistente_DeveRetornarNotFound()
        {
            // Arrange
            var bookId = 1;
            var existingBook = new BookEntity { Id = bookId, Title = "Livro", AuthorId = 1, GenreId = 1 };

            _bookRepositoryMock
                .Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(existingBook);

            _authorRepositoryMock
                .Setup(r => r.ExistsAsync(999))
                .ReturnsAsync(false);

            var command = new UpdateBookCommand
            {
                Id = bookId,
                Dto = new UpdateBookDto
                {
                    Title = "Qualquer",
                    PublicationDate = DateTime.Now,
                    AuthorId = 999,
                    GenreId = 1
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);
        }

        [Fact]
        public async Task Handle_GeneroInexistente_DeveRetornarNotFound()
        {
            // Arrange
            var bookId = 1;
            var existingBook = new BookEntity { Id = bookId, Title = "Livro", AuthorId = 1, GenreId = 1 };

            _bookRepositoryMock
                .Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(existingBook);

            _authorRepositoryMock
                .Setup(r => r.ExistsAsync(1))
                .ReturnsAsync(true);

            _genreRepositoryMock
                .Setup(r => r.ExistsAsync(999))
                .ReturnsAsync(false);

            var command = new UpdateBookCommand
            {
                Id = bookId,
                Dto = new UpdateBookDto
                {
                    Title = "Qualquer",
                    PublicationDate = DateTime.Now,
                    AuthorId = 1,
                    GenreId = 999
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);
        }

        [Fact]
        public async Task Handle_TituloDuplicadoMesmoAutor_DeveRetornarInvalid()
        {
            // Arrange
            var bookId = 1;
            var existingBook = new BookEntity { Id = bookId, Title = "Livro", AuthorId = 1, GenreId = 1 };

            _bookRepositoryMock
                .Setup(r => r.GetByIdAsync(bookId))
                .ReturnsAsync(existingBook);

            _authorRepositoryMock
                .Setup(r => r.ExistsAsync(1))
                .ReturnsAsync(true);

            _genreRepositoryMock
                .Setup(r => r.ExistsAsync(1))
                .ReturnsAsync(true);

            _bookRepositoryMock
                .Setup(r => r.ExistsByTitleAndAuthorAsync("Dom Casmurro", 1, bookId))
                .ReturnsAsync(true);

            var command = new UpdateBookCommand
            {
                Id = bookId,
                Dto = new UpdateBookDto
                {
                    Title = "Dom Casmurro",
                    PublicationDate = DateTime.Now,
                    AuthorId = 1,
                    GenreId = 1
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.Invalid);
            result.Message.Should().Contain("Já existe");

            _bookRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<BookEntity>()), Times.Never);
        }
    }
}
