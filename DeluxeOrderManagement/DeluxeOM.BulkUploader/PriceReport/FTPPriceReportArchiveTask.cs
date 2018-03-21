using System;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;
using DeluxeOM.Utils;
using System.IO;

namespace DeluxeOM.PR.BulkUploader
{
    /// <summary>
    /// Methods for Archive Price Report
    /// </summary>
    public class FTPPriceReportArchiveTask : ITask
    {
        private dlxTaskResult _taskResult { get; set; }
        IBulkUploadService _searvice = null;

        /// <summary>
        /// Create instance for BulkUploadService
        /// </summary>
        public FTPPriceReportArchiveTask()
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
                return "PriceReport FTP File Archive Task";
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
        /// Archive Price Report file on FTP
        /// </summary>
        /// <param name="id">QC Report task id</param>
        public void Execute(int id)
        {
            FTPUtils utils = null;
            JobItem jobItem = new JobItem();
            jobItem.StartDate = DateTime.UtcNow;
            jobItem.JobId = id;
            jobItem.TaskName = Name;
            try
            {
                var taskresult = this.PreviousTaskResult.TaskData;

                var ftpConfig = _searvice.GetFtpConfig(FtpConfigurationType.PriceReport);
                utils = new FTPUtils(ftpConfig);
                bool success = utils.Archive(new FileInfo(taskresult.FilePath).Name);

                if (!success)
                {
                    _taskResult = new dlxTaskResult()
                    {
                        IsSuccess = false,
                        ErrorMessage = "PriceReport Archive Failed."
                    };
                    jobItem.Status = false;
                    jobItem.Description = _taskResult.ErrorMessage;
                    jobItem.EndDate = DateTime.UtcNow;
                    _searvice.JobItemLog(jobItem);
                }
                else
                {
                    _taskResult = new dlxTaskResult()
                    {
                        IsSuccess = true,
                        TaskData = taskresult
                    };
                    jobItem.Status = true;
                    jobItem.Description = "PriceReport Archive Successful";
                    jobItem.EndDate = DateTime.UtcNow;
                    _searvice.JobItemLog(jobItem);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "PriceReport Archive Failed"
                };
                jobItem.Status = false;
                //jobItem.Description = _taskResult.ErrorMessage;
                jobItem.Description = string.Format("Error: {0} Inner Exception: {1} Error Message: {2}", _taskResult.ErrorMessage, ex.InnerException == null ? "" : ex.InnerException.ToString(), ex.Message);
                jobItem.EndDate = DateTime.UtcNow;
                _searvice.JobItemLog(jobItem);
            };

        }


        /// <summary>
        /// Return current task result
        /// </summary>
        /// <returns></returns>
        public dlxTaskResult GetTaskResult()
        {
            return _taskResult;
        }

        private dlxTaskResult Process()
        {
            //code to call SP
            return new dlxTaskResult();
        }
    }
}
