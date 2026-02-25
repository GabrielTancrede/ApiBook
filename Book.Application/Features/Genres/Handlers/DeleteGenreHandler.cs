using Book.Application.Features.Genres.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class DeleteGenreHandler : IRequestHandler<DeleteGenreCommand, ValidationResult<bool>>
    {
        private readonly IGenreRepository _genreRepository;

        public DeleteGenreHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ValidationResult<bool>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<bool>();

            var genre = await _genreRepository.GetByIdAsync(request.Id);
            if (genre == null)
                return validation.NotFound("Gênero não encontrado.");

            var hasBooks = await _genreRepository.HasBooksAsync(request.Id);
            if (hasBooks)
                return validation.Invalid("Não é possível excluir o gênero pois existem livros associados.");

            await _genreRepository.RemoveAsync(genre);
            await _genreRepository.SaveChangesAsync();

            return validation.Ok(true, "Gênero excluído com sucesso.");
        }
    }
}
