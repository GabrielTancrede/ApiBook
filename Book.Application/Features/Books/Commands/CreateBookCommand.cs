using Book.Application.DTOs.Book;
using Book.Application.ViewModels.Book;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Books.Commands
{
    public class CreateBookCommand : IRequest<ValidationResult<BookViewModel>>
    {
        public CreateBookDto Dto { get; set; } = new();
    }
}
