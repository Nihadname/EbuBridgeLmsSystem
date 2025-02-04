using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Exceptions
{
    public class CustomException : Exception
    {
        public int Code { get; set; }
        public Dictionary<string, string> Errors { get; set; } = new();


        public CustomException(int code, string message) : base(message)
        {
            Code = code;
        }

        public CustomException(string errorKey, string errorMessage)
        {
            Errors.Add(errorKey, errorMessage);
        }

        public CustomException(int code, Dictionary<string, string> errors, string message = null)
        {
            Code = code;
            Errors = errors;
            if (!string.IsNullOrEmpty(message))
            {
                base.HelpLink = message;
            }
        }

        public CustomException(int code, string errorKey, string errorMessage, string message = null) : base(message)
        {
            Code = code;
            Errors.Add(errorKey, errorMessage);
        }
    }
}
