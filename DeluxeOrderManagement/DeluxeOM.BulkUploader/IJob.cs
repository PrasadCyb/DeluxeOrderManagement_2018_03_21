using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.BulkUploader
{
    public interface IJob : IJobNotifications
    {
        JobStatus Process();

        string RunBy { get; set; }

        DateTime? FirstAnnouncedDate { get; set; }
        
    }

    public interface IJobNotifications
    {
        void ProcessNotifications(JobStatus jobStatus);
    }
}
