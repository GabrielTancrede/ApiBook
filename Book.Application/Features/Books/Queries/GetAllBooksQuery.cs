using Book.Application.Common;
using Book.Application.ViewModels.Book;
using MediatR;

namespace Book.Application.Features.Books.Queries
{
    public record GetAllBooksQuery : IRequest<Result<List<BookViewModel>>>;
}
