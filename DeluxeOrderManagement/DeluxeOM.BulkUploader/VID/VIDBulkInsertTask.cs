using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;

namespace DeluxeOM.VID.BulkUploader
{
    public class VIDBulkInsertTask : ITask 
    {
        IBulkUploadService _service = null;
        public VIDBulkInsertTask()
        {
            _service = new BulkUploadService();
        }

        public string Name
        {
            get
            {
                return "VID Bulk Insert Task";
            }
        }
        private dlxTaskResult _taskResult { get; set; }
        public dlxTaskResult PreviousTaskResult
        {
            get; set;
        }

        public void Execute(int id)
        {
            JobItem jobItem = new JobItem();
            jobItem.StartDate = DateTime.UtcNow;
            jobItem.JobId = id;
            jobItem.TaskName = Name;
            try
            {
                var taskresult = this.PreviousTaskResult.TaskData;
                _service.PopulateVID(taskresult.FilePath);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = true,
                };
                jobItem.Status = true;
                jobItem.Description = "VID Bulk Insert Successful";
                jobItem.EndDate = DateTime.UtcNow;
                _service.JobItemLog(jobItem);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "VID Bulk Insert Failed"
                };
                jobItem.Status = false;
                jobItem.Description = _taskResult.ErrorMessage;
                jobItem.EndDate = DateTime.UtcNow;
                _service.JobItemLog(jobItem);
            }
        }

        public dlxTaskResult GetTaskResult()
        {
            return _taskResult;
        }
        
    }
}
