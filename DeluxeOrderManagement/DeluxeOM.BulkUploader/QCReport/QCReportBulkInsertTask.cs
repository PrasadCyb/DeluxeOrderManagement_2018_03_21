using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;

namespace DeluxeOM.QC.BulkUploader
{
    /// <summary>
    /// Methods for QC report bulk insert
    /// </summary>
    public class QCReportBulkInsertTask : ITask 
    {
        IBulkUploadService _service = null;
        string _fileName = string.Empty;
        /// <summary>
        /// Create instance for BulkUploadService 
        /// </summary>
        /// <param name="fileName">QC report file name</param>
        public QCReportBulkInsertTask(string fileName)
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
                return "QCReport Bulk Insert Task";
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
        /// Bulk insert QC Report file into a staging table
        /// </summary>
        /// <param name="id">QC Report task id</param>
        public void Execute(int id)
        {
            JobItem jobItem = new JobItem();
            jobItem.StartDate = DateTime.UtcNow;
            jobItem.JobId = id;
            jobItem.TaskName = Name;
            try
            {
                var taskresult = this.PreviousTaskResult.TaskData;

                var ftpConfig = _service.GetFtpConfig(FtpConfigurationType.QCReport );
                string filePath = string.IsNullOrEmpty(_fileName) ? taskresult.FilePath
                    : string.Format("{0}{1}/{2}", ftpConfig.DownloadLocalDirectory, ftpConfig.FtpDirecrory, _fileName);

                

                int recordsImported = _service.PopulateQCReport(filePath);
                var validationResult = _service.ValidateQCReport();
                if (!validationResult.IsSucceeded)
                {
                    throw new Exception(validationResult.Message);
                }
                //taskresult.NoOfRecordImported = recordsImported;
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = true,
                    TaskData = string.IsNullOrEmpty(_fileName) ? taskresult : new dlxTaskData { NoOfRecordImported = recordsImported, FilePath = filePath }
                };
                jobItem.Status = true;
                jobItem.Description = "QC Report Bulk Insert Successful";
                jobItem.EndDate = DateTime.UtcNow;
                _service.JobItemLog(jobItem);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "QC report Bulk Insert Failed"
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
