using Book.Application.ModelInputs;
using Book.Application.ViewModels.Author;
using Book.Core.Common;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Authors.Queries
{
    public class GetPagedAuthorsQuery : PaginationRequest, IRequest<ValidationResult<PagedList<AuthorViewModel>>> { }
}
