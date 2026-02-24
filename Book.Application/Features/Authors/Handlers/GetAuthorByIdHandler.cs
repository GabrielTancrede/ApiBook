using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Authors.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Author;
using MediatR;

namespace Book.Application.Features.Authors.Handlers
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, Result<AuthorViewModel>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetAuthorByIdHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<Result<AuthorViewModel>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetByIdAsync(request.Id);
            
            if (author == null)
                return Result<AuthorViewModel>.Fail("Autor não encontrado.");

            var viewModel = _mapper.Map<AuthorViewModel>(author);
            return Result<AuthorViewModel>.Ok(viewModel);
        }
    }
}
