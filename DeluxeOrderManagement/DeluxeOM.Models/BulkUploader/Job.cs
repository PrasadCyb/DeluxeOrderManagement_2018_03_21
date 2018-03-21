using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.BulkUploader
{
    public class Job
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FileName{ get; set; }

        public string JobType { get; set; }
        public bool? Status { get; set; }

        public string StatusDisplay
        {
            get
            {
                if (Status.HasValue)
                    return Status.Value ? "Success" : "Failed";

                return "Processing";
            }
        }

        public string Description { get; set; }
        public string TriggeredBy { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

    }
}
