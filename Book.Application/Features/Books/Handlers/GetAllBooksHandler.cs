using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Books.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Book;
using MediatR;

namespace Book.Application.Features.Books.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, Result<List<BookViewModel>>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetAllBooksHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<BookViewModel>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllWithRelationsAsync();
            var viewModels = _mapper.Map<List<BookViewModel>>(books);
            return Result<List<BookViewModel>>.Ok(viewModels);
        }
    }
}
