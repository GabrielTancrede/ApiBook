using Book.Application.Features.Authors.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Authors.Handlers
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, ValidationResult<bool>>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<ValidationResult<bool>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<bool>();
            var author = await _authorRepository.GetByIdAsync(request.Id);

            if (author == null)
                return validation.NotFound("Autor não encontrado.");

            var hasBooks = await _authorRepository.HasBooksAsync(request.Id);
            if (hasBooks)
                return validation.Invalid("Não é possível excluir o autor pois existem livros associados.");

            await _authorRepository.RemoveAsync(author);
            await _authorRepository.SaveChangesAsync();

            return validation.Ok(true, "Autor excluído com sucesso.");
        }
    }
}
