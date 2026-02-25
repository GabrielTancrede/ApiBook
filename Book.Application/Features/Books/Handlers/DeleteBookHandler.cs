using Book.Application.Features.Books.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Books.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, ValidationResult<bool>>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ValidationResult<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<bool>();

            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
                return validation.NotFound("Livro não encontrado.");

            await _bookRepository.RemoveAsync(book);
            await _bookRepository.SaveChangesAsync();

            return validation.Ok(true, "Livro excluído com sucesso.");
        }
    }
}
