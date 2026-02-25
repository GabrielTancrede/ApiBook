using Book.Application.Features.Books.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Book;
using Book.Core.ValueObjects;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Books.Handlers
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, ValidationResult<BookViewModel>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public UpdateBookHandler(
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

        public async Task<ValidationResult<BookViewModel>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<BookViewModel>();

            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
                return validation.NotFound("Livro n�o encontrado.");

            var authorExists = await _authorRepository.ExistsAsync(request.Dto.AuthorId);
            if (!authorExists)
                return validation.NotFound("Autor n�o encontrado.");

            var genreExists = await _genreRepository.ExistsAsync(request.Dto.GenreId);
            if (!genreExists)
                return validation.NotFound("Gênero não encontrado.");

            var duplicateBook = await _bookRepository.ExistsByTitleAndAuthorAsync(request.Dto.Title, request.Dto.AuthorId, request.Id);
            if (duplicateBook)
                return validation.Invalid("Já existe um livro com este título para o mesmo autor.");

            book.Title = request.Dto.Title;
            book.Description = request.Dto.Description;
            book.PublicationDate = request.Dto.PublicationDate;
            book.ISBN = request.Dto.ISBN;
            book.AuthorId = request.Dto.AuthorId;
            book.GenreId = request.Dto.GenreId;
            book.UpdatedAt = DateTime.UtcNow;

            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveChangesAsync();

            var updatedBook = await _bookRepository.GetByIdWithRelationsAsync(book.Id);
            var viewModel = _mapper.Map<BookViewModel>(updatedBook);

            return validation.Ok(viewModel, "Livro atualizado com sucesso.");
        }
    }
}
