using Book.Core.Enum;
using Microsoft.AspNetCore.Mvc;
using Book.Core.ValueObjects;
using System.Collections;

namespace Book.Api.Configurations
{
    [ApiController]
    [Area("api")]
    public abstract class CustomControllerBase : ControllerBase
    {
        private ActionResult ResultError(string message, int statusCode = 400)
        {
            var errorResponse = new ErrorResponse(
                statusCode,
                message,
                HttpContext.Request.Path.Value ?? string.Empty
            );

            return StatusCode(statusCode, errorResponse);
        }

        public ActionResult QueryResult<T>(ValidationResult<T> obj)
        {
            return obj.ResultType switch
            {
                ResultType.Success => Ok(obj.Object),
                ResultType.Invalid => BadRequest(obj.Message),
                ResultType.NotFound => NotFound(obj.Message),
                _ => ResultError(obj.Message),
            };
        }

        public ActionResult CreatedResult<T>(ValidationResult<T> validation)
        {
            if (!validation.Success)
            {
                var statusCode = validation.ResultType switch
                {
                    ResultType.NotFound => 404,
                    ResultType.Invalid => 400,
                    _ => 500
                };

                return ResultError(validation.Message, statusCode);
            }

            return StatusCode(201, validation.Object);
        }

        public ActionResult UpdateResult<T>(ValidationResult<T> validation)
        {
            return validation.ResultType switch
            {
                ResultType.Success => Ok(validation.Object),
                ResultType.NotFound => NotFound(validation.Message),
                ResultType.Invalid => BadRequest(validation.Message),
                _ => ResultError(validation.Message),
            };
        }

        public ActionResult DeletedResult<T>(ValidationResult<T> validation)
        {
            return validation.ResultType switch
            {
                ResultType.Success => NoContent(),
                ResultType.NotFound => NotFound(validation.Message),
                ResultType.Invalid => BadRequest(validation.Message),
                _ => ResultError(validation.Message),
            };
        }
    }
}
