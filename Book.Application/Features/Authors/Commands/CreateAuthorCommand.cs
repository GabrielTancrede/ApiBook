using Book.Application.ViewModels.Author;
using Book.Application.DTOs.Author;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Authors.Commands
{
    public class CreateAuthorCommand : IRequest<ValidationResult<AuthorViewModel>>
    {
        public CreateAuthorDto Dto { get; set; } = new();
    }
}
