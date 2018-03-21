using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;

namespace DeluxeOM.Announcement.BulkUploader
{
    /// <summary>
    /// Methods for process announcement
    /// </summary>
    public class ProcessAnnouncementTask : ITask
    {
        IBulkUploadService _searvice = null;
        DateTime? _firstAnnouncedDate = null;

        /// <summary>
        /// Create instance for BulkUploadService
        /// </summary>
        /// <param name="firstAnnouncedDate">first announcement date</param>
        public ProcessAnnouncementTask(DateTime? firstAnnouncedDate)
        {
            _searvice = new BulkUploadService();
            _firstAnnouncedDate = firstAnnouncedDate;
        }

        /// <summary>
        /// Returns current task name
        /// </summary>
        public string Name
        {
            get
            {
                return "Process Announcement Task";
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
        /// Process Announcment from staging to main table
        /// </summary>
        /// <param name="id">Announcement task id</param>
        public void Execute(int id)
        {
            JobItem jobItem = new JobItem();
            jobItem.StartDate = DateTime.UtcNow;
            jobItem.JobId = id;
            jobItem.TaskName = Name;
            try
            {
                var taskresult = this.PreviousTaskResult.TaskData;

                _searvice.InsertAnnouncement(id, _firstAnnouncedDate);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = true,
                    //TaskData = new dlxTaskData() { FilePath = taskresult.FilePath }
                    TaskData = taskresult
                };
                jobItem.Status = true;
                jobItem.Description = "Process Announcement Successful";
                jobItem.EndDate = DateTime.UtcNow;
                _searvice.JobItemLog(jobItem);
                _searvice.InsertJobAnnouncement(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _taskResult = new dlxTaskResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "Process Announcement Task Failed"
                };
                jobItem.Status = false;
                //jobItem.Description = _taskResult.ErrorMessage;
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

        private dlxTaskResult Process()
        {
            //call SP that calculations, update fields etc
            return new dlxTaskResult();
        }
    }
}
