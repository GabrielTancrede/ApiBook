using Book.Application.Interfaces.Repositories;
using Book.Application.Features.Books.Queries;
using Book.Application.ViewModels.Book;
using Book.Core.ValueObjects;
using AutoMapper;
using MediatR;

namespace Book.Application.Features.Books.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, ValidationResult<BookViewModel>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIdHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ValidationResult<BookViewModel>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var validation = new ValidationResult<BookViewModel>();

            var book = await _bookRepository.GetByIdWithRelationsAsync(request.Id);

            if (book is null)
                return validation.NotFound("Livro não encontrado.");

            var viewModel = _mapper.Map<BookViewModel>(book);

            return validation.Ok(viewModel);
        }
    }
}
