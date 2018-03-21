using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;
using DeluxeOM.BulkUploader;

namespace DeluxeOM.QC.BulkUploader
{

    /// <summary>
    /// Methods for process Price Report
    /// </summary>
    public class ProcessPriceReportTask : ITask
    {
        IBulkUploadService _searvice = null;
        private dlxTaskResult _taskResult { get; set; }
        /// <summary>
        /// Create instance for BulkUploadService
        /// </summary>
        public ProcessPriceReportTask()
        {
            _searvice = new BulkUploadService();
        }

        /// <summary>
        /// Returns current task name
        /// </summary>
        public string Name
        {
            get
            {
                return "Process Price Report Task";
            }
        }

        /// <summary>
        /// GET and SET previous Task result
        /// </summary>
        public dlxTaskResult PreviousTaskResult
        {
            get; set;
        }

        /// <summary>
        /// Price Report from staging to main table
        /// </summary>
        /// <param name="id">Price Report task id</param>
        public void Execute(int id)
        {
            JobItem jobItem = new JobItem();
            jobItem.StartDate = DateTime.UtcNow;
            jobItem.JobId = id;
            jobItem.TaskName = Name;
            try
            {
                var taskresult = this.PreviousTaskResult.TaskData;
                _searvice.InsertPriceReport();
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = true,
                    TaskData = taskresult
                };
                jobItem.Status = true;
                jobItem.Description = "Process Price Report Successful";
                jobItem.EndDate = DateTime.UtcNow;
                _searvice.JobItemLog(jobItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "Process Price Report Task Failed"
                };
                jobItem.Status = false;
                jobItem.Description = string.Format("Error: {0} Inner Exception: {1} Error Message: {2}", _taskResult.ErrorMessage, ex.InnerException == null ? "" : ex.InnerException.ToString(), ex.Message);
                jobItem.EndDate = DateTime.UtcNow;
                _searvice.JobItemLog(jobItem);

            }
        }

        /// <summary>
        /// Return current task result
        /// </summary>
        /// <returns></returns>
        public dlxTaskResult GetTaskResult()
        {
            return _taskResult;
        }
    }
}
