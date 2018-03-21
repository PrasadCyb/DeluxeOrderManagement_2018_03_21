using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Repository
{
    public class JobsRepository : IJobsRepository 
    {

        /// <summary>
        /// Get all job executed from database
        /// </summary>
        /// <returns>List of Job</returns>
        public List<JOB> GetAll()
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var jobEty = _context.JOBS.OrderByDescending(x=>x.Id)
                    .ToList();
                return jobEty;
            }
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JOB GetById(int id)
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var jobety = _context.JOBS
                    .FirstOrDefault(x => x.Id == id );
                return jobety;
            }
        }

        /// <summary>
        /// Search details of selected job
        /// </summary>
        /// <param name="jobId">Id of selected job</param>
        /// <returns>Job details</returns>
        public List<Jobs_Items> GetJobsItems(int jobId)
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var jobety = _context.Jobs_Items
                    .Where(x => x.JobId == jobId)
                    .ToList();
                return jobety;
            }
        }

        
    }
}
