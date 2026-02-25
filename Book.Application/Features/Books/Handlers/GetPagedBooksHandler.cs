using Book.Application.Features.Books.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Book;
using Book.Core.ValueObjects;
using Book.Core.Common;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Books.Handlers
{
    public class GetPagedBooksHandler : IRequestHandler<GetPagedBooksQuery, ValidationResult<PagedList<BookViewModel>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public GetPagedBooksHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<PagedList<BookViewModel>>> Handle(GetPagedBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.SearchPaged(request.Page, request.PageSize);
            var pagedList = _mapper.Map<PagedList<BookViewModel>>(books);

            return new ValidationResult<PagedList<BookViewModel>>().Ok(pagedList);
        }
    }
}
