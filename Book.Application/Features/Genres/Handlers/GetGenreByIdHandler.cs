using Book.Application.Features.Genres.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using Book.Core.ValueObjects;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class GetGenreByIdHandler : IRequestHandler<GetGenreByIdQuery, ValidationResult<GenreViewModel>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GetGenreByIdHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<GenreViewModel>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<GenreViewModel>();

            var genre = await _genreRepository.GetByIdAsync(request.Id);
            if (genre == null)
                return validation.NotFound("Gênero não encontrado.");

            var viewModel = _mapper.Map<GenreViewModel>(genre);
            return validation.Ok(viewModel);
        }
    }
}
