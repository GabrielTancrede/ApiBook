using Book.Api.Configurations;
using Book.Application.DTOs.Book;
using Book.Application.Features.Books.Commands;
using Book.Application.Features.Books.Queries;
using Book.Application.ModelInputs;
using Book.Application.ViewModels.Book;
using Book.Core.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers
{
    [ApiController]
    [Route("api/v1/books")]
    public class BooksController : CustomControllerBase
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<BookViewModel>>> GetPaged([FromQuery] PaginationRequest pagination)
        {
            var result = await _mediator.Send(new GetPagedBooksQuery 
            { 
                Page = pagination.Page,
                PageSize = pagination.PageSize
            });

            return QueryResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewModel>> GetById(int id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery
            {
                Id = id
            });
                        
            return QueryResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<BookViewModel>> Create([FromBody] CreateBookDto dto)
        {
            var result = await _mediator.Send(new CreateBookCommand
            {
                Dto = dto
            });
            
            return CreatedResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookViewModel>> Update(int id, [FromBody] UpdateBookDto dto)
        {
            var result = await _mediator.Send(new UpdateBookCommand
            {
                Dto = dto,
                Id = id
            });
            
            return UpdateResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand
            {
                Id = id
            });
            
            return DeletedResult(result);
        }
    }
}
