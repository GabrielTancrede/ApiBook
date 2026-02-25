using Book.Application.ModelInputs;
using Book.Application.ViewModels.Genre;
using Book.Core.Common;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Genres.Queries
{
    public class GetPagedGenresQuery : PaginationRequest, IRequest<ValidationResult<PagedList<GenreViewModel>>> { } 
}
