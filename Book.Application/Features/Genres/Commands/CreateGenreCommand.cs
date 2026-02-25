using Book.Application.DTOs.Genre;
using Book.Application.ViewModels.Genre;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Genres.Commands
{
    public class CreateGenreCommand : IRequest<ValidationResult<GenreViewModel>>
    {
        public CreateGenreDto Dto { get; set; } = new();
    }
}
