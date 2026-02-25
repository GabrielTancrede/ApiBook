namespace Book.Core.ValueObjects
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public DateTime Timestamp { get; set; }

        public ErrorResponse(int statusCode, string message, string path)
        {
            StatusCode = statusCode;
            Message = message;
            Path = path;
            Timestamp = DateTime.UtcNow;
        }
    }
}
