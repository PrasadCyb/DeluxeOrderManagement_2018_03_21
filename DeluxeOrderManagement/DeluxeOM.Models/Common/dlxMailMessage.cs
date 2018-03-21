using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.Common
{
    public class dlxMailMessage
    {
        public  string From { get; set; }
        public string To { get; set; }
        public string SuccessSubject { get; set; }
        public string FailureSubject { get; set; }
        public string SuccessBody { get; set; }
        public string FailureBody { get; set; }
        public JobNotificationType NotificationTYpe { get; set; }
        public bool IsActive { get; set; }
    }
}
