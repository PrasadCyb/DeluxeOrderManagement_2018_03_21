using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Repository
{
    public interface IJobsRepository
    {
        List<JOB> GetAll();
        JOB GetById(int id);
        List<Jobs_Items> GetJobsItems(int jobId);
        
    }
}
