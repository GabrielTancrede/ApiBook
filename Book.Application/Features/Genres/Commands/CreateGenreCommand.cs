using Book.Application.Common;
using Book.Application.DTOs.Genre;
using Book.Application.ViewModels.Genre;
using MediatR;

namespace Book.Application.Features.Genres.Commands
{
    public record CreateGenreCommand(CreateGenreDto Dto) : IRequest<Result<GenreViewModel>>;
}
