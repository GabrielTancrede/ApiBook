using Book.Application.ModelInputs;
using Book.Application.ViewModels.Book;
using Book.Core.Common;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Books.Queries
{
    public class GetPagedBooksQuery : PaginationRequest, IRequest<ValidationResult<PagedList<BookViewModel>>> { }
}
