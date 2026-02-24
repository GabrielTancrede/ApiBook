using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Books.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Book;
using MediatR;
using BookEntity = Book.Core.Entites.Book;

namespace Book.Application.Features.Books.Handlers
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Result<BookViewModel>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public CreateBookHandler(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<Result<BookViewModel>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            // Validate Author exists
            var authorExists = await _authorRepository.ExistsAsync(request.Dto.AuthorId);
            if (!authorExists)
                return Result<BookViewModel>.Fail("Autor não encontrado.");

            // Validate Genre exists
            var genreExists = await _genreRepository.ExistsAsync(request.Dto.GenreId);
            if (!genreExists)
                return Result<BookViewModel>.Fail("Gênero não encontrado.");

            var book = new BookEntity
            {
                Title = request.Dto.Title,
                Description = request.Dto.Description,
                PublicationDate = request.Dto.PublicationDate,
                ISBN = request.Dto.ISBN,
                AuthorId = request.Dto.AuthorId,
                GenreId = request.Dto.GenreId,
                CreatedAt = DateTime.UtcNow
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            // Reload with relations
            var createdBook = await _bookRepository.GetByIdWithRelationsAsync(book.Id);
            var viewModel = _mapper.Map<BookViewModel>(createdBook);
            return Result<BookViewModel>.Ok(viewModel, "Livro criado com sucesso.");
        }
    }
}
