using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Genres.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class UpdateGenreHandler : IRequestHandler<UpdateGenreCommand, Result<GenreViewModel>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public UpdateGenreHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<Result<GenreViewModel>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetByIdAsync(request.Dto.Id);

            if (genre == null)
                return Result<GenreViewModel>.Fail("Gênero não encontrado.");

            genre.Name = request.Dto.Name;
            genre.Description = request.Dto.Description;
            genre.UpdatedAt = DateTime.UtcNow;

            await _genreRepository.UpdateAsync(genre);
            await _genreRepository.SaveChangesAsync();

            var viewModel = _mapper.Map<GenreViewModel>(genre);
            return Result<GenreViewModel>.Ok(viewModel, "Gênero atualizado com sucesso.");
        }
    }
}
