using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Books.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Book;
using MediatR;

namespace Book.Application.Features.Books.Handlers
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Result<BookViewModel>>
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

        public async Task<Result<BookViewModel>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Dto.Id);

            if (book == null)
                return Result<BookViewModel>.Fail("Livro não encontrado.");

            // Validate Author exists
            var authorExists = await _authorRepository.ExistsAsync(request.Dto.AuthorId);
            if (!authorExists)
                return Result<BookViewModel>.Fail("Autor não encontrado.");

            // Validate Genre exists
            var genreExists = await _genreRepository.ExistsAsync(request.Dto.GenreId);
            if (!genreExists)
                return Result<BookViewModel>.Fail("Gênero não encontrado.");

            book.Title = request.Dto.Title;
            book.Description = request.Dto.Description;
            book.PublicationDate = request.Dto.PublicationDate;
            book.ISBN = request.Dto.ISBN;
            book.AuthorId = request.Dto.AuthorId;
            book.GenreId = request.Dto.GenreId;
            book.UpdatedAt = DateTime.UtcNow;

            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveChangesAsync();

            // Reload with relations
            var updatedBook = await _bookRepository.GetByIdWithRelationsAsync(book.Id);
            var viewModel = _mapper.Map<BookViewModel>(updatedBook);
            return Result<BookViewModel>.Ok(viewModel, "Livro atualizado com sucesso.");
        }
    }
}
