using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeluxeOM.Models
{
    public class ErrorModel
    {
        public int HttpStatusCode { get; set; }

        public Exception Exception { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}