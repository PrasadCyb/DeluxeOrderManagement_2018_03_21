using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;

namespace DeluxeOM.PR.BulkUploader
{

    /// <summary>
    /// Methods for Price report bulk insert
    /// </summary>
    public class PriceReportBulkInsertTask : ITask 
    {
        IBulkUploadService _service = null;
        string _fileName = string.Empty;
        /// <summary>
        /// Create instance for BulkUploadService 
        /// </summary>
        /// <param name="fileName">Price report file name</param>
        public PriceReportBulkInsertTask(string fileName)
        {
            _service = new BulkUploadService();
            _fileName = fileName;
        }

        /// <summary>
        /// Returns current task name
        /// </summary>
        public string Name
        {
            get
            {
                return "PriceReport Bulk Insert Task";
            }
        }
        private dlxTaskResult _taskResult { get; set; }

        /// <summary>
        /// GET and SET previous Task result
        /// </summary>
        public dlxTaskResult PreviousTaskResult
        {
            get; set;
        }


        /// <summary>
        /// Bulk insert Price Report file into a staging table
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

                var ftpConfig = _service.GetFtpConfig(FtpConfigurationType.PriceReport);
                string filePath = string.IsNullOrEmpty(_fileName) ? taskresult.FilePath
                    : string.Format("{0}{1}/{2}", ftpConfig.DownloadLocalDirectory, ftpConfig.FtpDirecrory, _fileName);

                int recordsImported =  _service.PopulatePriceReport(filePath);

                //validate staging data
                var validationResult = _service.ValidatePriceReport();
                if (!validationResult.IsSucceeded)
                {
                    throw new Exception(validationResult.Message);
                }

                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = true,
                    TaskData = string.IsNullOrEmpty(_fileName) ? taskresult : new dlxTaskData { NoOfRecordImported = recordsImported, FilePath = filePath }
                };
                jobItem.Status = true;
                jobItem.Description = "Price Report Bulk Insert Successful";
                jobItem.EndDate = DateTime.UtcNow;
                _service.JobItemLog(jobItem);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "Price report Bulk Insert Failed"
                };
                jobItem.Status = false;
                jobItem.Description = string.Format("Error: {0} Inner Exception: {1} Error Message: {2}", _taskResult.ErrorMessage, ex.InnerException == null ? "" : ex.InnerException.ToString(), ex.Message);
                jobItem.EndDate = DateTime.UtcNow;
                _service.JobItemLog(jobItem);
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
