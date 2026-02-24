using Book.Application.Common;
using Book.Application.DTOs.Author;
using Book.Application.ViewModels.Author;
using MediatR;

namespace Book.Application.Features.Authors.Commands
{
    public record CreateAuthorCommand(CreateAuthorDto Dto) : IRequest<Result<AuthorViewModel>>;
}
