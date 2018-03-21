using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;

namespace DeluxeOM.PO.BulkUploader
{

    /// <summary>
    /// Methods for PipeLine order bulk insert
    /// </summary>
    public class PipeLineOrderBulkInsertTask : ITask 
    {
        IBulkUploadService _service = null;
        string _fileName = string.Empty;

        /// <summary>
        /// Create instance for BulkUploadService 
        /// </summary>
        /// <param name="fileName">PipeLine order file name</param>
        public PipeLineOrderBulkInsertTask( string fileName)
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
                return "PileLineOrder Bulk Insert Task";
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
        /// Bulk insert PipeLine order file into a staging table
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

                var ftpConfig = _service.GetFtpConfig(FtpConfigurationType.PipelineOrder);
                string filePath = string.IsNullOrEmpty(_fileName) ? taskresult.FilePath
                    : string.Format("{0}{1}/{2}", ftpConfig.DownloadLocalDirectory, ftpConfig.FtpDirecrory, _fileName);

                int recordsImported = _service.PopulatePipeLineOrder(filePath);
                //taskresult.NoOfRecordImported = recordsImported;
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = true,
                    TaskData = string.IsNullOrEmpty(_fileName) ? taskresult: new dlxTaskData { NoOfRecordImported=recordsImported,FilePath=filePath}
                };
                
                jobItem.Status = true;
                jobItem.Description = "Pipe line order Bulk Insert Successful";
                jobItem.EndDate = DateTime.UtcNow;
                _service.JobItemLog(jobItem);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "Pipe line order Bulk Insert Failed"
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
