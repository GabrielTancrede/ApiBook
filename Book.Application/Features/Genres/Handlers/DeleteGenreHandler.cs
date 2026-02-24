using Book.Application.Common;
using Book.Application.Features.Genres.Commands;
using Book.Application.Interfaces.Repositories;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class DeleteGenreHandler : IRequestHandler<DeleteGenreCommand, Result>
    {
        private readonly IGenreRepository _genreRepository;

        public DeleteGenreHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetByIdAsync(request.Id);

            if (genre == null)
                return Result.Fail("Gênero não encontrado.");

            var hasBooks = await _genreRepository.HasBooksAsync(request.Id);
            if (hasBooks)
                return Result.Fail("Não é possível excluir o gênero pois existem livros associados.");

            await _genreRepository.RemoveAsync(genre);
            await _genreRepository.SaveChangesAsync();

            return Result.Ok("Gênero excluído com sucesso.");
        }
    }
}
