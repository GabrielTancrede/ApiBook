using Book.Application.Common;
using Book.Application.DTOs.Author;
using Book.Application.Features.Authors.Commands;
using Book.Application.Features.Authors.Queries;
using Book.Application.ViewModels.Author;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todos os autores
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Result<List<AuthorViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAuthorsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtém um autor pelo ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Result<AuthorViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<AuthorViewModel>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAuthorByIdQuery(id));
            
            if (!result.Success)
                return NotFound(result);
            
            return Ok(result);
        }

        /// <summary>
        /// Cria um novo autor
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Result<AuthorViewModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<AuthorViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
        {
            var result = await _mediator.Send(new CreateAuthorCommand(dto));
            
            if (!result.Success)
                return BadRequest(result);
            
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        /// <summary>
        /// Atualiza um autor existente
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Result<AuthorViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<AuthorViewModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<AuthorViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorDto dto)
        {
            if (id != dto.Id)
                return BadRequest(Result<AuthorViewModel>.Fail("ID da rota não corresponde ao ID do corpo da requisição."));

            var result = await _mediator.Send(new UpdateAuthorCommand(dto));
            
            if (!result.Success)
                return NotFound(result);
            
            return Ok(result);
        }

        /// <summary>
        /// Exclui um autor
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand(id));
            
            if (!result.Success)
            {
                if (result.Errors.Any(e => e.Contains("não encontrado")))
                    return NotFound(result);
                return BadRequest(result);
            }
            
            return Ok(result);
        }
    }
}
