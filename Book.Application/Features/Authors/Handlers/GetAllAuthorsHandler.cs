using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Authors.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Author;
using MediatR;

namespace Book.Application.Features.Authors.Handlers
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, Result<List<AuthorViewModel>>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetAllAuthorsHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<AuthorViewModel>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<AuthorViewModel>>(authors);
            return Result<List<AuthorViewModel>>.Ok(viewModels);
        }
    }
}
