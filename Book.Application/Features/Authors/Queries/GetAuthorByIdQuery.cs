using Book.Application.Common;
using Book.Application.ViewModels.Author;
using MediatR;

namespace Book.Application.Features.Authors.Queries
{
    public record GetAuthorByIdQuery(int Id) : IRequest<Result<AuthorViewModel>>;
}
