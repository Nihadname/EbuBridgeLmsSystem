using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Domain.Entities.Common
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }

        private Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        // Common Errors
        public static readonly Error NotFound = new("NotFound", "The requested item was not found.");
        public static readonly Error ValidationFailed = new("ValidationFailed", "Validation failed for the input.");
        public static readonly Error Unauthorized = new("Unauthorized", "You are not authorized to perform this action.");
        public static readonly Error InternalServerError = new("InternalServerError", "An unexpected error occurred.");
        public static readonly Error SystemError = new("SystemError", "A critical system error occurred.");

        // Client-Side Errors
        public static readonly Error BadRequest = new("BadRequest", "The request is invalid or malformed.");
        public static readonly Error Conflict = new("Conflict", "The request conflicts with an existing resource.");
        public static readonly Error Forbidden = new("Forbidden", "You do not have permission to access this resource.");

        // Server-Side Errors
        public static readonly Error ServiceUnavailable = new("ServiceUnavailable", "The service is temporarily unavailable. Please try again later.");
        public static readonly Error Timeout = new("Timeout", "The request took too long to process and timed out.");

        // Customizable error creation for dynamic messages
        public static Error Custom(string code, string message) => new(code, message);
    }
}
