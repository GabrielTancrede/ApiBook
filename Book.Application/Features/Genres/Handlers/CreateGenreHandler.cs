using Book.Application.Features.Genres.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using Book.Core.ValueObjects;
using Book.Core.Entites;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class CreateGenreHandler : IRequestHandler<CreateGenreCommand, ValidationResult<GenreViewModel>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public CreateGenreHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<GenreViewModel>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
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
            return new ValidationResult<GenreViewModel>().Ok(viewModel, "Gênero criado com sucesso.");
        }
    }
}
