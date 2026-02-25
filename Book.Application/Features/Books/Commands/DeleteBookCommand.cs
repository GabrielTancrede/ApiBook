using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Books.Commands
{
    public class DeleteBookCommand : IRequest<ValidationResult<bool>>
    {
        public int Id { get; set; }
    }
}
