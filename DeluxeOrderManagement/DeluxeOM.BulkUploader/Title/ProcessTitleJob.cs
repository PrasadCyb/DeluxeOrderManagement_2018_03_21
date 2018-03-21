using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Services;

namespace DeluxeOM.Title.BulkUploader
{

    /// <summary>
    /// Methods for process Title task
    /// </summary>   
    public class ProcessTitleJob : IJob
    {
        //FtpConfiguration _config = null;
        List<ITask> _tasks = null;
        BulkUploadService _service = null;
        public ProcessTitleJob(List<ITask> tasks)
        {
            //_config = config;
            _tasks = tasks;
            _service = new BulkUploadService();
        }

        /// <summary>
        /// Process all task of Titles
        /// </summary>
        /// <returns>JobStatus</returns>
        public JobStatus Process()
        {
            #region Previous Code
            //int maxCount = _tasks.Count();
            //int jobCount = 0;
            //dlxTaskResult taskResult = dlxTaskResult.SuccessResult();
            //JobStatus jobStatus = JobStatus.DlxSuccessResult();
            //Job jobLog = new Job();
            //jobLog.JobType = FtpConfigurationType.ProcessTitleReport.ToString();
            //int jobId = _service.JobLog(jobLog);
            //jobLog.TriggeredBy = this.RunBy;
            //foreach (var task in _tasks)
            //{
            //    jobCount++;
            //    if (taskResult.IsSuccess)
            //    {
            //        task.PreviousTaskResult = taskResult;
            //        task.Execute(jobId);
            //        taskResult = task.GetTaskResult();
            //        if (jobCount == maxCount && taskResult.IsSuccess)
            //        {
            //            jobLog.Status = true;
            //            jobLog.Description = "Job Successfully Completed";
            //            jobLog.Id = jobId;
            //            _service.UpdateJobLog(jobLog);
            //        }
            //        if (jobCount == maxCount && !taskResult.IsSuccess)
            //        {
            //            jobLog.Status = false;
            //            jobLog.Description = "Job fails due to" + taskResult.ErrorMessage;
            //            jobLog.Id = jobId;
            //            _service.UpdateJobLog(jobLog);
            //        }
            //        //System.Threading.Thread.Sleep(2000);
            //    }
            //    else
            //    {
            //        jobLog.Status = false;
            //        jobLog.Description = "Job fails due to" + taskResult.ErrorMessage;
            //        jobLog.Id = jobId;
            //        _service.UpdateJobLog(jobLog);
            //        break;
            //    }
            //}
            //return jobStatus; 
            int jobCount = 0;
            dlxTaskResult taskResult = dlxTaskResult.SuccessResult();
            JobStatus jobStatus = JobStatus.DlxSuccessResult();
            Job jobLog = new Job();
            jobLog.JobType = FtpConfigurationType.ProcessTitleReport.ToString();
            jobLog.TriggeredBy = this.RunBy;
            int jobId = _service.JobLog(jobLog);
            jobStatus.JobId = jobId;
            string runingTask = string.Empty;

            foreach (var task in _tasks)
            {
                jobCount++;
                runingTask = task.Name;
                if (taskResult.IsSuccess)
                {
                    task.PreviousTaskResult = taskResult;
                    task.Execute(jobId);
                    taskResult = task.GetTaskResult();
                }
                else
                {
                    jobStatus.Success = false;
                    jobLog.Description = "Job fails due to" + " " + taskResult.ErrorMessage;
                    break;
                }

                if (taskResult.TaskData != null && !string.IsNullOrEmpty(taskResult.TaskData.FilePath) && string.IsNullOrEmpty(jobLog.FileName))
                {
                    string filePath = taskResult.TaskData.FilePath;
                    jobLog.FileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
                }
            }

            jobLog.Status = jobStatus.Success;
            jobLog.Description = jobStatus.Success ? "Job completed successfully" : string.Format("Job failed due to {0} task failed", runingTask);
            jobLog.Id = jobId;
            _service.UpdateJobLog(jobLog);

            return jobStatus;
            #endregion
        }

        public string RunBy { get; set; }

        public DateTime? FirstAnnouncedDate { get; set; }


        /// <summary>
        /// Send notification email to specific users on process of title job
        /// </summary>
        /// <param name="jobStatus"></param>
        public void ProcessNotifications(JobStatus jobStatus)
        {
            //Send notifications only when job failed. As this job runs hourly basis, there will be lot of emails sent
            if (!jobStatus.Success)
            {
                _service.ProcessNotifications(Models.JobNotificationType.ProcessTitleReportNotification, jobStatus);
            }
        }
    }
}
