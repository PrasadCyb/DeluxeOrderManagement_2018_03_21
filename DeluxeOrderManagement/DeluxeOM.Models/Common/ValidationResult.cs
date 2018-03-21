using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.Common
{
    public class dlxValidationResult
    {
        public dlxValidationResult()
        {
            this.IsSucceeded = false;
            this.Message = string.Empty;
        }

        public string Source { get; set; }
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }

        public static dlxValidationResult SuccessResult
        {
            get
            {
                return new dlxValidationResult() { IsSucceeded = true };
            }
        }

        public static dlxValidationResult FailureResult(string message)
        {
            return new dlxValidationResult() { IsSucceeded = false, Message = message };

        }

    }
}
