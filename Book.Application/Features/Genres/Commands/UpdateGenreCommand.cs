using Book.Application.DTOs.Genre;
using Book.Application.ViewModels.Genre;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Genres.Commands
{
    public class UpdateGenreCommand : IRequest<ValidationResult<GenreViewModel>>
    {
        public UpdateGenreDto Dto { get; set; } = new();
        public int Id { get; set; }
    }
}
