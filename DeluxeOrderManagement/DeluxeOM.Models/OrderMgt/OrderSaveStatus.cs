using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class OrderSaveStatus
    {
        public string ErrorMessage { get; set; }
        public bool IsSaved { get; set; }
        public bool IsAnnouncementExist { get; set; }
    }
}
