using System;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;
using DeluxeOM.BulkUploader;

namespace DeluxeOM.PO.BulkUploader
{
    /// <summary>
    /// Methods for process PipeLine order
    /// </summary>
    public class ProcessPipeLineOrderTask : ITask
    {
        IBulkUploadService _searvice = null;
        private dlxTaskResult _taskResult { get; set; }

        /// <summary>
        /// Create instance for BulkUploadService
        /// </summary>
        public ProcessPipeLineOrderTask()
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
                return "Process Pipe Line Order Task";
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
        /// Process Pipeline order from staging to main table
        /// </summary>
        /// <param name="id">PipeLine order task id</param>
        public void Execute(int id)
        {
            JobItem jobItem = new JobItem();
            jobItem.StartDate = DateTime.UtcNow;
            jobItem.JobId = id;
            jobItem.TaskName = Name;
            try
            {
                var taskresult = this.PreviousTaskResult.TaskData;

                _searvice.InsertPipeLineOrder();
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = true,
                    TaskData = taskresult
                };
                jobItem.Status = true;
                jobItem.Description = "Process Pipe Line Order Successful";
                jobItem.EndDate = DateTime.UtcNow;
                _searvice.JobItemLog(jobItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "Process Pipe Line Order Task Failed"
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
