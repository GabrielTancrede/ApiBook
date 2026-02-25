using Book.Core.ValueObjects;
using MediatR;

namespace Book.Application.Features.Genres.Commands
{
    public class DeleteGenreCommand : IRequest<ValidationResult<bool>>
    {
        public int Id { get; set; }
    }
}
