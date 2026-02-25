using Book.Application.Features.Genres.Commands;
using Book.Application.Features.Genres.Queries;
using Book.Application.ViewModels.Genre;
using Book.Application.ModelInputs;
using Book.Application.DTOs.Genre;
using Microsoft.AspNetCore.Mvc;
using Book.Api.Configurations;
using Book.Core.Common;
using MediatR;

namespace Book.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : CustomControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<GenreViewModel>>> GetPaged([FromQuery] PaginationRequest pagination)
        {
            var result = await _mediator.Send(new GetPagedGenresQuery
            {
                Page = pagination.Page,
                PageSize = pagination.PageSize
            });

            return QueryResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreViewModel>> GetById(int id)
        {
            var result = await _mediator.Send(new GetGenreByIdQuery
            {
                Id = id
            });
            
            return QueryResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<GenreViewModel>> Create([FromBody] CreateGenreDto dto)
        {
            var result = await _mediator.Send(new CreateGenreCommand
            {
                Dto = dto
            });
            
            return CreatedResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GenreViewModel>> Update(int id, [FromBody] UpdateGenreDto dto)
        {
            var result = await _mediator.Send(new UpdateGenreCommand
            {
                Dto = dto,
                Id = id
            });
            
            return UpdateResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteGenreCommand
            {
                Id = id
            });
            
            return DeletedResult(result);
        }
    }
}
