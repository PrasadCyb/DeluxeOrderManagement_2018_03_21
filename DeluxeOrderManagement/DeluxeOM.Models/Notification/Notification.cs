using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Enabled { get; set; }
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string Description { get; set; }
    }
}
