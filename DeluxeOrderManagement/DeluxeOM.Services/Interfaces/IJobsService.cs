using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Services
{
    public interface IJobsService
    {
        List<Job> GetAll();
        Job GetById(int id);
        List<JobItem> GetJobsItems(int jobId);

        void Run(string fileType, DateTime announcementDate, string loggedinUser,string fileName);
        string GetFileUploadPath(string jobType, string fileName);


    }
}
