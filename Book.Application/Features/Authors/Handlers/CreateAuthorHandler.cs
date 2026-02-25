using Book.Application.Features.Authors.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Author;
using Book.Core.ValueObjects;
using Book.Core.Entites;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Authors.Handlers
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, ValidationResult<AuthorViewModel>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public CreateAuthorHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<AuthorViewModel>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Name = request.Dto.Name,
                Biography = request.Dto.Biography,
                BirthDate = request.Dto.BirthDate,
                CreatedAt = DateTime.UtcNow
            };

            await _authorRepository.AddAsync(author);
            await _authorRepository.SaveChangesAsync();

            var viewModel = _mapper.Map<AuthorViewModel>(author);
            return new ValidationResult<AuthorViewModel>().Ok(viewModel, "Autor criado com sucesso.");
        }
    }
}
