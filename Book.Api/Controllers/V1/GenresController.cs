using Book.Application.Common;
using Book.Application.DTOs.Genre;
using Book.Application.Features.Genres.Commands;
using Book.Application.Features.Genres.Queries;
using Book.Application.ViewModels.Genre;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todos os gêneros
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Result<List<GenreViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllGenresQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtém um gênero pelo ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Result<GenreViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<GenreViewModel>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetGenreByIdQuery(id));
            
            if (!result.Success)
                return NotFound(result);
            
            return Ok(result);
        }

        /// <summary>
        /// Cria um novo gênero
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Result<GenreViewModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<GenreViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateGenreDto dto)
        {
            var result = await _mediator.Send(new CreateGenreCommand(dto));
            
            if (!result.Success)
                return BadRequest(result);
            
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        /// <summary>
        /// Atualiza um gênero existente
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Result<GenreViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<GenreViewModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result<GenreViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGenreDto dto)
        {
            if (id != dto.Id)
                return BadRequest(Result<GenreViewModel>.Fail("ID da rota não corresponde ao ID do corpo da requisição."));

            var result = await _mediator.Send(new UpdateGenreCommand(dto));
            
            if (!result.Success)
                return NotFound(result);
            
            return Ok(result);
        }

        /// <summary>
        /// Exclui um gênero
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteGenreCommand(id));
            
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
