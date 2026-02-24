using Book.Application.Common;
using Book.Application.DTOs.Book;
using Book.Application.Features.Books.Commands;
using Book.Application.Features.Books.Queries;
using Book.Application.ViewModels.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todos os livros
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Result<List<BookViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllBooksQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtém um livro pelo ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Result<BookViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<BookViewModel>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery(id));
            
            if (!result.Success)
                return NotFound(result);
            
            return Ok(result);
        }

        /// <summary>
        /// Cria um novo livro
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Result<BookViewModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<BookViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            var result = await _mediator.Send(new CreateBookCommand(dto));
            
            if (!result.Success)
                return BadRequest(result);
            
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        /// <summary>
        /// Atualiza um livro existente
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Result<BookViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<BookViewModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<BookViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
        {
            if (id != dto.Id)
                return BadRequest(Result<BookViewModel>.Fail("ID da rota não corresponde ao ID do corpo da requisição."));

            var result = await _mediator.Send(new UpdateBookCommand(dto));
            
            if (!result.Success)
                return NotFound(result);
            
            return Ok(result);
        }

        /// <summary>
        /// Exclui um livro
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand(id));
            
            if (!result.Success)
                return NotFound(result);
            
            return Ok(result);
        }
    }
}
