namespace Book.Application.Common
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();

        public static Result<T> Ok(T data, string? message = null)
        {
            return new Result<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }

        public static Result<T> Fail(string error)
        {
            return new Result<T>
            {
                Success = false,
                Errors = new List<string> { error }
            };
        }

        public static Result<T> Fail(List<string> errors)
        {
            return new Result<T>
            {
                Success = false,
                Errors = errors
            };
        }
    }

    public class Result
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();

        public static Result Ok(string? message = null)
        {
            return new Result
            {
                Success = true,
                Message = message
            };
        }

        public static Result Fail(string error)
        {
            return new Result
            {
                Success = false,
                Errors = new List<string> { error }
            };
        }
    }
}
