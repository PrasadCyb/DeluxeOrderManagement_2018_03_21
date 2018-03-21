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
    /// Methods for process Price report task
    /// </summary>
    public class PriceReportUploaderJob : IJob
    {
        //FtpConfiguration _config = null;
        List<ITask> _tasks = null;
        BulkUploadService _service = null;
        /// <summary>
        /// Initialize task for Price report
        /// </summary>
        /// <param name="tasks">Tasks for Price report</param>
        public PriceReportUploaderJob(List<ITask> tasks)
        {
            //_config = config;
            _tasks = tasks;
            _service = new BulkUploadService(); 
        }



        /// <summary>
        /// Process all task of Price report
        /// </summary>
        /// <returns>JobStatus</returns>
        public JobStatus Process()
        {
            #region Previous Code
            //int jobCount = 0;
            //dlxTaskResult taskResult = dlxTaskResult.SuccessResult();
            //JobStatus jobStatus = JobStatus.DlxSuccessResult();
            //Job jobLog = new Job();
            //jobLog.JobType = FtpConfigurationType.PriceReport.ToString();
            //jobLog.TriggeredBy = this.RunBy;
            //int jobId = _service.JobLog(jobLog);
            //jobStatus.JobId = jobId;

            //foreach (var task in _tasks)
            //{
            //    jobCount++;
            //    if (taskResult.IsSuccess)
            //    {
            //        task.PreviousTaskResult = taskResult;
            //        task.Execute(jobId);
            //        taskResult = task.GetTaskResult();
            //        //System.Threading.Thread.Sleep(2000);
            //        if (jobCount == _tasks.Count() && taskResult.IsSuccess)
            //        {
            //            jobLog.Status = true;
            //            jobLog.Description = "Job Successfully Completed";
            //            jobLog.Id = jobId;
            //            _service.UpdateJobLog(jobLog);
            //        }
            //        if (jobCount == _tasks.Count() && !taskResult.IsSuccess)
            //        {
            //            jobLog.Status = false;
            //            jobLog.Description = "Job fails due to"+ " " + taskResult.ErrorMessage;
            //            jobLog.Id = jobId;
            //            _service.UpdateJobLog(jobLog);
            //        }
            //    }
            //    else
            //    {
            //        jobLog.Status = false;
            //        jobLog.Description = "Job fails due to" + " " + taskResult.ErrorMessage;
            //        jobLog.Id = jobId;
            //        _service.UpdateJobLog(jobLog);
            //        break;
            //    }
            //}  
            #endregion

            int jobCount = 0;
            dlxTaskResult taskResult = dlxTaskResult.SuccessResult();
            JobStatus jobStatus = JobStatus.DlxSuccessResult();
            Job jobLog = new Job();
            jobLog.JobType = FtpConfigurationType.PriceReport.ToString();
            jobLog.TriggeredBy = this.RunBy;
            int jobId = _service.JobLog(jobLog);
            jobStatus.JobId = jobId;
            string runingTask = string.Empty;
            int recordsImported = 0;

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

                if (taskResult.TaskData != null )
                {
                    if (!string.IsNullOrEmpty(taskResult.TaskData.FilePath) && string.IsNullOrEmpty(jobLog.FileName))
                    {
                        string filePath = taskResult.TaskData.FilePath;
                        jobLog.FileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
                    }
                    
                    if (taskResult.TaskData.NoOfRecordImported > 0)
                        recordsImported = taskResult.TaskData.NoOfRecordImported;
                }
            }

            jobLog.Status = taskResult.IsSuccess;
            jobLog.Description = taskResult.IsSuccess ? "Job completed successfully" : string.Format("Job failed due to {0} task failed", runingTask);
            jobLog.Id = jobId;
            _service.UpdateJobLog(jobLog);

            jobStatus.FileName = jobLog.FileName;
            jobStatus.NoOfRecordsImported = recordsImported;
            jobStatus.Success = taskResult.IsSuccess;
            return jobStatus;
        }

        public string RunBy { get; set; }

        public DateTime? FirstAnnouncedDate { get; set; }

        /// <summary>
        /// Send notification email to specific users on import of Price report
        /// </summary>
        /// <param name="jobStatus"></param>
        public void ProcessNotifications(JobStatus jobStatus)
        {
            _service.ProcessNotifications(Models.JobNotificationType.LoadPriceReportNotification, jobStatus);
        }

    }

    
}
