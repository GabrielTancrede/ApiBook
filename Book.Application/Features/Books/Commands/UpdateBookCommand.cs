using Book.Application.Common;
using Book.Application.DTOs.Book;
using Book.Application.ViewModels.Book;
using MediatR;

namespace Book.Application.Features.Books.Commands
{
    public record UpdateBookCommand(UpdateBookDto Dto) : IRequest<Result<BookViewModel>>;
}
