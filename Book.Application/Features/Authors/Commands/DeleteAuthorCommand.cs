using Book.Application.Common;
using MediatR;

namespace Book.Application.Features.Authors.Commands
{
    public record DeleteAuthorCommand(int Id) : IRequest<Result>;
}
