using Book.Application.ViewModels.Author;
using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Authors.Queries
{
    public class GetAuthorByIdQuery : IRequest<ValidationResult<AuthorViewModel>> 
    { 
        public int Id { get; set; }
    }
}
