using AutoMapper;
using Book.Application.Features.Genres.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using Book.Core.Common;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class GetPagedGenresHandler : IRequestHandler<GetPagedGenresQuery, ValidationResult<PagedList<GenreViewModel>>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GetPagedGenresHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<PagedList<GenreViewModel>>> Handle(GetPagedGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _genreRepository.SearchPaged(request.Page, request.PageSize, request.Paged);
            var pagedList = _mapper.Map<PagedList<GenreViewModel>>(genres);
            
            return new ValidationResult<PagedList<GenreViewModel>>().Ok(pagedList);
        }
    }
}
