using Book.Application.ViewModels.Genre;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Genres.Queries
{
    public record GetGenreByIdQuery : IRequest<ValidationResult<GenreViewModel>>
    {
        public int Id { get; set; }
    }
}
