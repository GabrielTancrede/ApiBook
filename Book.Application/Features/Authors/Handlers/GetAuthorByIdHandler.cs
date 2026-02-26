using Book.Application.Features.Authors.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Author;
using Book.Core.ValueObjects;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Authors.Handlers
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, ValidationResult<AuthorViewModel>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public GetAuthorByIdHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<AuthorViewModel>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<AuthorViewModel>();

            var author = await _authorRepository.GetByIdAsync(request.Id);

            if (author is null)
                return validation.NotFound("Autor não encontrado.");

            var viewModel = _mapper.Map<AuthorViewModel>(author);

            return validation.Ok(viewModel);
        }
    }
}
