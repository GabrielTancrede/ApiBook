using AutoMapper;
using Book.Application.Common;
using Book.Application.Features.Books.Queries;
using Book.Application.Interfaces.Repositories;
using Book.Application.ViewModels.Book;
using MediatR;

namespace Book.Application.Features.Books.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Result<BookViewModel>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIdHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Result<BookViewModel>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdWithRelationsAsync(request.Id);
            
            if (book == null)
                return Result<BookViewModel>.Fail("Livro não encontrado.");

            var viewModel = _mapper.Map<BookViewModel>(book);
            return Result<BookViewModel>.Ok(viewModel);
        }
    }
}
