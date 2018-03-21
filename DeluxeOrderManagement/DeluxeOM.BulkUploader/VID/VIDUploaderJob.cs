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
    public class VIDUploaderJob //: IJob
    {
        //FtpConfiguration _config = null;
        List<ITask> _tasks = null;
        BulkUploadService _service = null;
        public VIDUploaderJob(List<ITask> tasks)
        {
            //_config = config;
            _tasks = tasks;
            _service = new BulkUploadService();
        }

        public JobStatus Process()
        {
            int jobCount = 0;
            dlxTaskResult taskResult = dlxTaskResult.SuccessResult();
            Job jobLog = new Job();
            jobLog.JobType = FtpConfigurationType.VID.ToString();
            int jobId = _service.JobLog(jobLog);
            foreach (var task in _tasks)
            {
                jobCount++;
                if (taskResult.IsSuccess)
                {
                    task.PreviousTaskResult = taskResult;
                    task.Execute(jobId);
                    taskResult = task.GetTaskResult();
                    if (jobCount == 3 && taskResult.IsSuccess)
                    {
                        jobLog.Status = true;
                        jobLog.Description = "Job Successfully Completed";
                        jobLog.Id = jobId;
                        _service.UpdateJobLog(jobLog);
                    }
                    //System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    jobLog.Status = false;
                    jobLog.Description = "Job fails due to" + taskResult.ErrorMessage;
                    jobLog.Id = jobId;
                    _service.UpdateJobLog(jobLog);
                    break;
                }
            }
            return new JobStatus();
        }

        public string RunBy { get; set; }

        public DateTime? FirstAnnouncedDate { get; set; }

        public void ProcessNotifications(int jobId = 0)
        {

        }

    }

    
}
