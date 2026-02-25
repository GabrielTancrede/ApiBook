using Book.Application.Features.Authors.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Author;
using Book.Core.ValueObjects;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Authors.Handlers
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, ValidationResult<AuthorViewModel>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public UpdateAuthorHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<AuthorViewModel>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<AuthorViewModel>();
            var author = await _authorRepository.GetByIdAsync(request.Id);
            if (author == null)
                return validation.NotFound("Autor não encontrado.");

            var nameExists = await _authorRepository.ExistsByNameAsync(request.Dto.Name, request.Id);
            if (nameExists)
                return validation.Invalid("Já existe um autor com este nome.");

            author.Name = request.Dto.Name;
            author.Biography = request.Dto.Biography;
            author.BirthDate = request.Dto.BirthDate;
            author.UpdatedAt = DateTime.UtcNow;

            await _authorRepository.UpdateAsync(author);
            await _authorRepository.SaveChangesAsync();

            var viewModel = _mapper.Map<AuthorViewModel>(author);
            return validation.Ok(viewModel, "Autor atualizado com sucesso.");
        }
    }
}
