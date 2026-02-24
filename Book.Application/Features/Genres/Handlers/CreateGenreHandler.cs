using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Genres.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using Book.Core.Entites;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class CreateGenreHandler : IRequestHandler<CreateGenreCommand, Result<GenreViewModel>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public CreateGenreHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<Result<GenreViewModel>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre
            {
                Name = request.Dto.Name,
                Description = request.Dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _genreRepository.AddAsync(genre);
            await _genreRepository.SaveChangesAsync();

            var viewModel = _mapper.Map<GenreViewModel>(genre);
            return Result<GenreViewModel>.Ok(viewModel, "Gênero criado com sucesso.");
        }
    }
}
