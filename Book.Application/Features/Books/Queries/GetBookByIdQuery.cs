using Book.Application.ViewModels.Book;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Books.Queries
{
    public class GetBookByIdQuery : IRequest<ValidationResult<BookViewModel>>
    {
        public int Id { get; set; }
    }
}
