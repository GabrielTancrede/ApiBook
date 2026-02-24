using Book.Application.Common;
using Book.Application.ViewModels.Genre;
using MediatR;

namespace Book.Application.Features.Genres.Queries
{
    public record GetAllGenresQuery : IRequest<Result<List<GenreViewModel>>>;
}
