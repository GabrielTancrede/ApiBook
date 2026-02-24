using Book.Application.Common;
using MediatR;

namespace Book.Application.Features.Books.Commands
{
    public record DeleteBookCommand(int Id) : IRequest<Result>;
}
