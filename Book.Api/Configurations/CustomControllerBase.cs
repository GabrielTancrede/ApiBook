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
        private ActionResult ResultError(string message, int statusCode = 400, object? details = null)
        {
            var operationId = Guid.NewGuid().ToString();
            
            var errorMessage = statusCode == 500 
                ? "Ocorreu um erro interno no servidor. Por favor, tente novamente mais tarde." 
                : message;
            
            var errorResponse = new ErrorResponse(
                statusCode,
                errorMessage,
                HttpContext.Request.Path.Value ?? string.Empty,
                details,
                operationId
            );

            return StatusCode(statusCode, errorResponse);
        }

        public ActionResult QueryResult<T>(ValidationResult<T> obj)
        {
            return obj.ResultType switch
            {
                ResultType.Success => Ok(obj.Object),
                ResultType.Invalid => ResultError(obj.Message, 400),
                ResultType.NotFound => ResultError(obj.Message, 404),
                _ => ResultError(obj.Message, 500),
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
                ResultType.NotFound => ResultError(validation.Message, 404),
                ResultType.Invalid => ResultError(validation.Message, 400),
                _ => ResultError(validation.Message, 500),
            };
        }

        public ActionResult DeletedResult<T>(ValidationResult<T> validation)
        {
            return validation.ResultType switch
            {
                ResultType.Success => NoContent(),
                ResultType.NotFound => ResultError(validation.Message, 404),
                ResultType.Invalid => ResultError(validation.Message, 400),
                _ => ResultError(validation.Message, 500),
            };
        }
    }
}
