using Book.Core.ValueObjects;
using System.Net;
using System.Text.Json;

namespace Book.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var operationId = Guid.NewGuid().ToString();

            var errorResponse = new ErrorResponse(
                statusCode: (int)HttpStatusCode.InternalServerError,
                message: "Ocorreu um erro interno no servidor. Por favor, tente novamente mais tarde.",
                path: context.Request.Path.Value ?? string.Empty,
                details: null,
                operationId: operationId
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(errorResponse, options);

            return context.Response.WriteAsync(json);
        }
    }
}
