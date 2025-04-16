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
        public SuccessReturnType? SuccessReturnType { get; private set; }
        public T Data { get; private set; }

        private Result(bool isSuccess, Error error, T data, ErrorType? errorType = null,SuccessReturnType? successReturnType=null, List<string> errors=null)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorType = errorType;
            SuccessReturnType = successReturnType;
            Errors = errors;
            Error= error;
        }
        public List<string> Errors { get; private set; }
 
        public static Result<T> Success(T data,SuccessReturnType? successReturnType) => new(true,null, data,null,successReturnType);
        public static Result<T> Failure(Error error, List<string> errors, ErrorType errorType) => new(false, error, default, errorType,null, errors);

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
    public enum SuccessReturnType
    {
        Created,
        NoContent,

    }
}
