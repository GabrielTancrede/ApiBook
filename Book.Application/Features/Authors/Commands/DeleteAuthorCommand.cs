using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Authors.Commands
{
    public class DeleteAuthorCommand : IRequest<ValidationResult<bool>>
    {
        public int Id { get; set; }
    }
}
