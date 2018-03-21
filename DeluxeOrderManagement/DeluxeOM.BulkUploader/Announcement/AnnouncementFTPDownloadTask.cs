using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;
using DeluxeOM.Utils;

namespace DeluxeOM.Announcement.BulkUploader
{
    /// <summary>
    /// Methods for download announcement file
    /// </summary>
    public class AnnouncementFTPDownloadTask : ITask
    {
        private dlxTaskResult _taskResult { get; set; }
        IBulkUploadService _searvice = null;
        /// <summary>
        /// Create instance for BulkUploadService
        /// </summary>
        public AnnouncementFTPDownloadTask()
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
                return "Announcement FTP Download Task";
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
        /// Download announcement file from FTP
        /// </summary>
        /// <param name="id">Announcement task id</param>
        public void Execute(int id)
        {
            FTPUtils utils = null;
            JobItem jobItem = new JobItem();
            jobItem.StartDate = DateTime.UtcNow;
            jobItem.JobId = id;
            jobItem.TaskName = Name;
            try
            {
                var ftpConfig = _searvice.GetFtpConfig(FtpConfigurationType.Announcement);
                utils = new FTPUtils(ftpConfig);
                utils.DownloadFile();

                if (utils.DownloadFileName == null)
                {
                    _taskResult = new dlxTaskResult()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Announcement Download Failed as file is not exists"
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
                        TaskData = new dlxTaskData() { FilePath = utils.DownloadFileName }
                    };
                    jobItem.Status = true;
                    jobItem.Description = "Announcement Download Successful";
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
                    ErrorMessage = "Announcement Download Failed",
                    TaskData = new dlxTaskData() { FilePath = utils.DownloadFileName }
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
