using Book.Core.Enum;

namespace Book.Core.ValueObjects
{
    public class ValidationResult
    {
        public bool Success { get; set; }

        public ResultType ResultType { get; set; }

        public string Message { get; set; }

        public ValidationResult()
        {
            Success = false;
        }

        public ValidationResult(ResultType resultType, string msg)
        {
            SetNewResult(resultType, msg);
        }

        public virtual ValidationResult SetNewResult(ResultType resultType, string msg)
        {
            ResultType = resultType;
            Success = resultType == ResultType.Success;
            Message = msg;
            return this;
        }

        public virtual ValidationResult Ok(string msg = "")
        {
            return SetNewResult(ResultType.Success, msg);
        }

        public virtual ValidationResult Invalid(string msg)
        {
            return SetNewResult(ResultType.Invalid, msg);
        }

        public virtual ValidationResult NotFound(string msg)
        {
            return SetNewResult(ResultType.NotFound, msg);
        }
    }

    public class ValidationResult<T> : ValidationResult
    {
        public T Object { get; set; }

        public ValidationResult()
        {
        }

        public ValidationResult(ResultType resultType, string msg)
            : base(resultType, msg)
        {
        }

        public ValidationResult<T> SetNewResult(ResultType resultType, string msg, T obj)
        {
            base.ResultType = resultType;
            base.Success = resultType == ResultType.Success;
            base.Message = msg;
            Object = obj;
            return this;
        }

        public ValidationResult<T> Ok(T obj, string msg = "")
        {
            SetNewResult(ResultType.Success, msg, obj);
            return this;
        }

        public ValidationResult<T> Invalid(string msg, T obj = default(T))
        {
            SetNewResult(ResultType.Invalid, msg, obj);
            return this;
        }

        public override ValidationResult Invalid(string msg)
        {
            SetNewResult(ResultType.Invalid, msg, default(T));
            return this;
        }

        public ValidationResult<T> NotFound(string msg, T obj = default(T))
        {
            SetNewResult(ResultType.NotFound, msg, obj);
            return this;
        }

        public override ValidationResult NotFound(string msg)
        {
            SetNewResult(ResultType.NotFound, msg, default(T));
            return this;
        }
    }
}
