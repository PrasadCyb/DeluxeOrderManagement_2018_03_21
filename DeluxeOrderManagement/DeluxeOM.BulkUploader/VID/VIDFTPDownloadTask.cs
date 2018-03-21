using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;
using DeluxeOM.Utils;

namespace DeluxeOM.VID.BulkUploader
{
    public class VIDFTPDownloadTask : ITask
    {
        IBulkUploadService _searvice = null;
        public VIDFTPDownloadTask()
        {
            _searvice = new BulkUploadService();
        }
        public string Name
        {
            get
            {
                return "VID FTP Download Task";
            }
        }

        private  dlxTaskResult _taskResult { get; set; }
        public dlxTaskResult PreviousTaskResult
        {
            get; set;
        }

        public void Execute(int id)
        {
            FTPUtils utils = null;
            JobItem jobItem = new JobItem();
            jobItem.StartDate = DateTime.UtcNow;
            jobItem.JobId = id;
            jobItem.TaskName = Name;
            try
            {
                var ftpConfig = _searvice.GetFtpConfig(FtpConfigurationType.VID);
                utils = new FTPUtils(ftpConfig);
                utils.DownloadFile();
                if (utils.DownloadFileName == null)
                {
                    _taskResult = new dlxTaskResult()
                    {
                        IsSuccess = false,
                        ErrorMessage = "VID Download Failed file is not exist"
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
                    jobItem.Description = "VID Download Successful";
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
                    ErrorMessage = "VID Download Failed"
                };
                jobItem.Status = false;
                jobItem.Description = _taskResult.ErrorMessage;
                jobItem.EndDate = DateTime.UtcNow;
                _searvice.JobItemLog(jobItem);
            };
        }

        public dlxTaskResult GetTaskResult()
        {
            return _taskResult;
        }

        



    }
}
