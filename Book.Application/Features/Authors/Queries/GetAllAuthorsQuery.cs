using Book.Application.Common;
using Book.Application.ViewModels.Author;
using MediatR;

namespace Book.Application.Features.Authors.Queries
{
    public record GetAllAuthorsQuery : IRequest<Result<List<AuthorViewModel>>>;
}
