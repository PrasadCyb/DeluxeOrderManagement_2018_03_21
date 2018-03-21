using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class SaveResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public SaveResult()
        {
            this.Success = false;
            this.Message = string.Empty;
        }

        public static SaveResult SuccessResult
        {
            get
            {
                return new SaveResult() { Success = true };
            }
        }

        public static SaveResult FailureResult(string message)
        {
            return new SaveResult() { Success = false , Message = message};
            
        }
    }
}
