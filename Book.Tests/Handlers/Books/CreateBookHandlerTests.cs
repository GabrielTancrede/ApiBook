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
    public class CreateBookHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateBookHandler _handler;

        public CreateBookHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _genreRepositoryMock = new Mock<IGenreRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new CreateBookHandler(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _genreRepositoryMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ComDadosValidos_DeveRetornarSucesso()
        {
            // Arrange
            var command = new CreateBookCommand
            {
                Dto = new CreateBookDto
                {
                    Title = "Dom Casmurro",
                    Description = "Romance clássico",
                    PublicationDate = new DateTime(1899, 1, 1),
                    ISBN = "978-3-16-148410-0",
                    AuthorId = 1,
                    GenreId = 1
                }
            };

            _authorRepositoryMock
                .Setup(r => r.ExistsAsync(1))
                .ReturnsAsync(true);

            _genreRepositoryMock
                .Setup(r => r.ExistsAsync(1))
                .ReturnsAsync(true);

            _bookRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<BookEntity>()))
                .Returns(Task.CompletedTask);

            _bookRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            var bookWithRelations = new BookEntity
            {
                Id = 1,
                Title = "Dom Casmurro",
                Description = "Romance clássico",
                PublicationDate = new DateTime(1899, 1, 1),
                ISBN = "978-3-16-148410-0",
                AuthorId = 1,
                GenreId = 1,
                Author = new Author { Id = 1, Name = "Machado de Assis" },
                Genre = new Genre { Id = 1, Name = "Romance" }
            };

            _bookRepositoryMock
                .Setup(r => r.GetByIdWithRelationsAsync(It.IsAny<int>()))
                .ReturnsAsync(bookWithRelations);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.ResultType.Should().Be(ResultType.Success);
            result.Object.Should().NotBeNull();
            result.Object.Title.Should().Be("Dom Casmurro");
            result.Object.AuthorName.Should().Be("Machado de Assis");
            result.Object.GenreName.Should().Be("Romance");

            _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<BookEntity>()), Times.Once);
            _bookRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_AutorInexistente_DeveRetornarNotFound()
        {
            // Arrange
            var command = new CreateBookCommand
            {
                Dto = new CreateBookDto
                {
                    Title = "Livro Teste",
                    PublicationDate = DateTime.Now,
                    AuthorId = 999,
                    GenreId = 1
                }
            };

            _authorRepositoryMock
                .Setup(r => r.ExistsAsync(999))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);

            _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<BookEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_GeneroInexistente_DeveRetornarNotFound()
        {
            // Arrange
            var command = new CreateBookCommand
            {
                Dto = new CreateBookDto
                {
                    Title = "Livro Teste",
                    PublicationDate = DateTime.Now,
                    AuthorId = 1,
                    GenreId = 999
                }
            };

            _authorRepositoryMock
                .Setup(r => r.ExistsAsync(1))
                .ReturnsAsync(true);

            _genreRepositoryMock
                .Setup(r => r.ExistsAsync(999))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.NotFound);

            _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<BookEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_TituloDuplicadoMesmoAutor_DeveRetornarInvalid()
        {
            // Arrange
            var command = new CreateBookCommand
            {
                Dto = new CreateBookDto
                {
                    Title = "Dom Casmurro",
                    PublicationDate = DateTime.Now,
                    AuthorId = 1,
                    GenreId = 1
                }
            };

            _authorRepositoryMock
                .Setup(r => r.ExistsAsync(1))
                .ReturnsAsync(true);

            _genreRepositoryMock
                .Setup(r => r.ExistsAsync(1))
                .ReturnsAsync(true);

            _bookRepositoryMock
                .Setup(r => r.ExistsByTitleAndAuthorAsync("Dom Casmurro", 1, null))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.ResultType.Should().Be(ResultType.Invalid);
            result.Message.Should().Contain("Já existe");

            _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<BookEntity>()), Times.Never);
        }
    }
}
