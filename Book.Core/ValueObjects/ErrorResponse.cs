namespace Book.Core.ValueObjects
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object? Details { get; set; }
        public DateTime Timestamp { get; set; }
        public string Path { get; set; }
        public string? OperationId { get; set; }

        public ErrorResponse(int statusCode, string message, string path, object? details = null, string? operationId = null)
        {
            StatusCode = statusCode;
            Message = message;
            Path = path;
            Details = details;
            Timestamp = DateTime.UtcNow;
            OperationId = operationId;
        }
    }
}
