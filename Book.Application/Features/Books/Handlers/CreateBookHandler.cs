using Book.Application.Features.Books.Commands;
using Book.Application.Interfaces.Repositories;
using BookEntity = Book.Core.Entites.Book;
using Book.Application.ViewModels.Book;
using Book.Core.ValueObjects;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Books.Handlers
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, ValidationResult<BookViewModel>>
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

        public async Task<ValidationResult<BookViewModel>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<BookViewModel>();
            
            var authorExists = await _authorRepository.ExistsAsync(request.Dto.AuthorId);
            if (!authorExists)
                return validation.NotFound("Autor não encontrado.");

            var genreExists = await _genreRepository.ExistsAsync(request.Dto.GenreId);
            if (!genreExists)
                return validation.NotFound("Gênero não encontrado.");

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

            var createdBook = await _bookRepository.GetByIdWithRelationsAsync(book.Id);
            var viewModel = _mapper.Map<BookViewModel>(createdBook);
            return validation.Ok(viewModel, "Livro criado com sucesso.");
        }
    }
}
