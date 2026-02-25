using Book.Application.Features.Authors.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Author;
using Book.Core.ValueObjects;
using Book.Core.Common;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Authors.Handlers
{
    public class GetPagedAuthorsHandler : IRequestHandler<GetPagedAuthorsQuery, ValidationResult<PagedList<AuthorViewModel>>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public GetPagedAuthorsHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<PagedList<AuthorViewModel>>> Handle(GetPagedAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.SearchPaged(request.Page, request.PageSize);
            var pagedList = _mapper.Map<PagedList<AuthorViewModel>>(authors);

            return new ValidationResult<PagedList<AuthorViewModel>>().Ok(pagedList);
        }
    }
}
