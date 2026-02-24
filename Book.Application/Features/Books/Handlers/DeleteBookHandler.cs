using Book.Application.Common;
using Book.Application.Features.Books.Commands;
using Book.Application.Interfaces.Repositories;
using MediatR;

namespace Book.Application.Features.Books.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book == null)
                return Result.Fail("Livro não encontrado.");

            await _bookRepository.RemoveAsync(book);
            await _bookRepository.SaveChangesAsync();

            return Result.Ok("Livro excluído com sucesso.");
        }
    }
}
