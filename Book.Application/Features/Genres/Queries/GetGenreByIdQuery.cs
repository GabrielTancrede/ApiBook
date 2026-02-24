using Book.Application.Common;
using Book.Application.ViewModels.Genre;
using MediatR;

namespace Book.Application.Features.Genres.Queries
{
    public record GetGenreByIdQuery(int Id) : IRequest<Result<GenreViewModel>>;
}
