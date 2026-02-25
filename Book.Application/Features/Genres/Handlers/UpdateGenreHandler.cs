using Book.Application.Features.Genres.Commands;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Genre;
using Book.Core.ValueObjects;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Genres.Handlers
{
    public class UpdateGenreHandler : IRequestHandler<UpdateGenreCommand, ValidationResult<GenreViewModel>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public UpdateGenreHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<GenreViewModel>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<GenreViewModel>();

            var genre = await _genreRepository.GetByIdAsync(request.Id);
            if (genre == null)
                return validation.NotFound("Gênero não encontrado.");

            var nameExists = await _genreRepository.ExistsByNameAsync(request.Dto.Name, request.Id);
            if (nameExists)
                return validation.Invalid("Já existe um gênero com este nome.");

            genre.Name = request.Dto.Name;
            genre.Description = request.Dto.Description;
            genre.UpdatedAt = DateTime.UtcNow;

            await _genreRepository.UpdateAsync(genre);
            await _genreRepository.SaveChangesAsync();

            var viewModel = _mapper.Map<GenreViewModel>(genre);
            return validation.Ok(viewModel, "G�nero atualizado com sucesso.");
        }
    }
}
