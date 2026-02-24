using Book.Application.Common;
using Book.Application.Features.Authors.Commands;
using Book.Application.Interfaces.Repositories;
using MediatR;

namespace Book.Application.Features.Authors.Handlers
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, Result>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id);

            if (author == null)
                return Result.Fail("Autor não encontrado.");

            var hasBooks = await _authorRepository.HasBooksAsync(request.Id);
            if (hasBooks)
                return Result.Fail("Não é possível excluir o autor pois existem livros associados.");

            await _authorRepository.RemoveAsync(author);
            await _authorRepository.SaveChangesAsync();

            return Result.Ok("Autor excluído com sucesso.");
        }
    }
}
