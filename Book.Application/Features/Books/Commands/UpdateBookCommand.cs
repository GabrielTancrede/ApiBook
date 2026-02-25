using Book.Application.DTOs.Book;
using Book.Application.ViewModels.Book;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Books.Commands
{
    public class UpdateBookCommand : IRequest<ValidationResult<BookViewModel>>
    {
        public int Id { get; set; }
        public UpdateBookDto Dto { get; set; } = new();
    }
}
