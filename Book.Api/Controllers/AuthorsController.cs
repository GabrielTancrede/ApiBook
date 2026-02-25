using Book.Api.Configurations;
using Book.Application.DTOs.Author;
using Book.Application.Features.Authors.Commands;
using Book.Application.Features.Authors.Queries;
using Book.Application.ModelInputs;
using Book.Application.ViewModels.Author;
using Book.Core.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : CustomControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<AuthorViewModel>>> GetPaged([FromQuery] PaginationRequest pagination)
        {
            var result = await _mediator.Send(new GetPagedAuthorsQuery 
            { 
                Page = pagination.Page,
                PageSize = pagination.PageSize
            });

            return QueryResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorViewModel>> GetById(int id)
        {
            var result = await _mediator.Send(new GetAuthorByIdQuery
            {
                Id = id
            });
            
            return QueryResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorViewModel>> Create([FromBody] CreateAuthorDto dto)
        {
            var result = await _mediator.Send(new CreateAuthorCommand
            {
                Dto = dto
            });
            
            return CreatedResult(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorViewModel>> Update(int id, [FromBody] UpdateAuthorDto dto)
        {
            var result = await _mediator.Send(new UpdateAuthorCommand
            {
                Id = id,
                Dto = dto
            });
            
            return UpdateResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand
            {
                Id = id
            });
            
            return DeletedResult(result);
        }
    }
}
