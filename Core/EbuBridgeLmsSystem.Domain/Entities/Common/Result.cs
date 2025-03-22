using EbuBridgeLmsSystem.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace LearningManagementSystem.Core.Entities.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public Error? Error { get; private set; }    
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ErrorType? ErrorType { get; private set; }
        public T Data { get; private set; }

        private Result(bool isSuccess, Error error, T data, ErrorType? errorType = null, List<string> errors=null)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorType = errorType;
            Errors = errors;
            Error= error;
        }
        public List<string> Errors { get; private set; }
 
        public static Result<T> Success(T data) => new(true,null, data);
        public static Result<T> Failure(Error error, List<string> errors, ErrorType errorType) => new(false, error, default, errorType, errors);

        public static Result<TOut> FailureResult<TIn, TOut>(Result<TIn> result)
        {
            return Result<TOut>.Failure(result.Error, result.Errors, (ErrorType)result.ErrorType);
        }

    }
    public enum ErrorType
    {
        ValidationError,
        BusinessLogicError,
        NotFoundError,
        UnauthorizedError,
        SystemError

    }
}
