using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.BulkUploader
{
    //This is a task
    public class JobItem
    {
        public int Id { get; set; }

        public int JobId { get; set; }
        public string TaskName { get; set; }
        public bool? Status { get; set; }
        public string StatusDisplay
        {
            get
            {
                if(Status.HasValue)
                    return Status.Value ? "Success" : "Failed";

                return string.Empty;
            }
        }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
