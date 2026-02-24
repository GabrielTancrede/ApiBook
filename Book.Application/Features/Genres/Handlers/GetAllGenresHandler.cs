using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Genres.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class GetAllGenresHandler : IRequestHandler<GetAllGenresQuery, Result<List<GenreViewModel>>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GetAllGenresHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GenreViewModel>>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _genreRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<GenreViewModel>>(genres);
            return Result<List<GenreViewModel>>.Ok(viewModels);
        }
    }
}
