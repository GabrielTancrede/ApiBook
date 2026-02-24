using Book.Application.Common;
using MediatR;

namespace Book.Application.Features.Genres.Commands
{
    public record DeleteGenreCommand(int Id) : IRequest<Result>;
}
