using Book.Application.ViewModels.Author;
using Book.Application.DTOs.Author;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Authors.Commands
{
    public class UpdateAuthorCommand : IRequest<ValidationResult<AuthorViewModel>>
    {
        public UpdateAuthorDto Dto { get; set; } = new();
        public int Id { get; set; }
    }
}
