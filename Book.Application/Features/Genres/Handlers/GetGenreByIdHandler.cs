using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Genres.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class GetGenreByIdHandler : IRequestHandler<GetGenreByIdQuery, Result<GenreViewModel>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GetGenreByIdHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<Result<GenreViewModel>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetByIdAsync(request.Id);
            
            if (genre == null)
                return Result<GenreViewModel>.Fail("Gênero não encontrado.");

            var viewModel = _mapper.Map<GenreViewModel>(genre);
            return Result<GenreViewModel>.Ok(viewModel);
        }
    }
}
