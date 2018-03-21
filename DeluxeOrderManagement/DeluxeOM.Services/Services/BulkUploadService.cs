using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Repository;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Models.Common;

namespace DeluxeOM.Services
{
    public class BulkUploadService : ServiceBase, DeluxeOM.Services.IBulkUploadService
    {
        IBulkUploadRepository _repository = null;
        INotificationService _nfnService = null;
        /// <summary>
        /// Create instance for BulkUploadRepository and NotificationService
        /// </summary>
        public BulkUploadService()
        {
            _repository = new BulkUploadRepository ();
            _nfnService = new NotificationService();
        }

        /// <summary>
        /// Fetch data for FTP configuation from Database
        /// </summary>
        /// <param name="configType"></param>
        /// <returns>FTPConfiguation</returns>
        public FtpConfiguration GetFtpConfig(FtpConfigurationType configType)
        {
            return _repository.GetFtpConfig(configType);
        }


        /// <summary>
        /// Bulk insert Announcement file into staging table
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        /// <returns>No record inserted into staging table</returns>
        public int PopulateAnnouncement(string fileName)
        {
           return _repository.BulkInsertAnnouncement(fileName);
        }

        /// <summary>
        /// Bulk insert PipeLine file into staging table
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        /// <returns>No record inserted into staging table</returns>
        public int PopulatePipeLineOrder(string fileName)
        {
            return _repository.BulkInsertPipeLineOrder(fileName);
        }


        /// <summary>
        /// Bulk insert Price Report file into staging table
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        /// <returns>No record inserted into staging table</returns>
        public int PopulatePriceReport(string fileName)
        {
            return _repository.BulkInsertPriceReport(fileName);
        }

        /// <summary>
        /// Bulk insert QC Report file into staging table
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        /// <returns>No record inserted into staging table</returns>
        public int PopulateQCReport(string fileName)
        {
            return _repository.BulkInsertQCReport(fileName);
        }

        /// <summary>
        /// Bulk insert VID file into staging table 
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        public void PopulateVID(string fileName)
        {
            _repository.BulkInsertVID(fileName);
        }

        /// <summary>
        /// Process Pipeline order into main table
        /// </summary>
        public void InsertPipeLineOrder()
        {
            _repository.InsertPipeLineOrder();
        }

        /// <summary>
        /// Process price report into main table
        /// </summary>
        public void InsertPriceReport()
        {
            _repository.InsertPriceReport();
        }

        /// <summary>
        /// Process QC report into main table
        /// </summary>
        public void InsertQCReport()
        {
            _repository.InsertQCReport();
        }

        /// <summary>
        /// Process announcemnet into main table
        /// </summary>
        /// <param name="id">JonId</param>
        /// <param name="firstAnouncedDate">First announcement date selected from UI</param>
        public void InsertAnnouncement(int id, DateTime? firstAnnouncedDate)
        {
            _repository.InsertAnnouncement(id, firstAnnouncedDate);
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="id"></param>
        public void InsertJobAnnouncement(int id)
        {
            _repository.InsertJobAnnouncement(id);
        }

        /// <summary>
        /// Process Title report into TitlesReport table
        /// </summary>
        public void ProcessTitleReport()
        {
            _repository.ProcessTitleReport();
        }


        /// <summary>
        /// Log current job into JOB table
        /// </summary>
        /// <param name="job"></param>
        /// <returns>jonID</returns>
        public int JobLog(Job jobModel)
        {
            JOB job = new JOB();
            job.JobType = jobModel.JobType;
            job.TriggeredBy = jobModel.TriggeredBy;
            int id=_repository.JobLog(job);
            return id;
        }

        /// <summary>
        /// Log corresponding task of job into Jobs_Item table
        /// </summary>
        /// <param name="jobItem"></param>
        public void JobItemLog(JobItem jobItemModel)
        {
            Jobs_Items jobItem = new Jobs_Items()
            {
                JobId = jobItemModel.JobId,
                StartDate = jobItemModel.StartDate,
                EndDate = jobItemModel.EndDate,
                Status = jobItemModel.Status,
                TaskName = jobItemModel.TaskName,
                Description = jobItemModel.Description
            };
            _repository.JobItemLog(jobItem);

        }

        /// <summary>
        /// Update current Job
        /// </summary>
        /// <param name="job"></param>
        public void UpdateJobLog(Job jobModel)
        {
            JOB job = new JOB();
            job.Id = jobModel.Id;
            job.Status = jobModel.Status;
            job.Description = jobModel.Description;
            job.FileName = jobModel.FileName;
            _repository.UpdateJobLog(job);
        }

        //public bool IsAnnouncementProcessed(string fileName)
        //{
        //    return _repository.IsAnnouncementProcessed(fileName);
        //}

        /// <summary>
        /// Send notification email to specific users on process of title job
        /// </summary>
        /// <param name="jobNotificationType">Type of notification o send</param>
        /// <param name="jobStatus">Success or Failure</param>
        public void ProcessNotifications(Models.JobNotificationType jobNotificationType, JobStatus jobStatus)
        {
            
            _nfnService.SendJobNotification(jobNotificationType,  jobStatus);
        }

        /// <summary>
        /// Send notification email to specific users on import of announcement
        /// </summary>
        /// <param name="jobStatus">Success or Failure</param>
        public void SendAnnouncementNewTitlesNotification(JobStatus jobStatus)
        {
            _nfnService.SendAnnouncementNewTitlesNotification(jobStatus);
        }

        /// <summary>
        /// Check whether announcement file is already processed or not
        /// </summary>
        /// <param name="fileName">announcement file name</param>
        /// <returns>Failure Result or Success result</returns>
        public dlxValidationResult ValidateAnnouncement(string fileName)
        {
            if (_repository.IsAnnouncementProcessed(fileName))
            {
                string msg = string.Format("File {0} is already processed. You can not process same file again.", fileName);
                return dlxValidationResult.FailureResult(msg);
            }

            if (!_repository.IsDependentFileProcessed())
            {
                string msg = string.Format("Announcement is not processed since latest dependent Price or QC file processing is failed. Please re-upload those failed file and process an Announcement again.");
                return dlxValidationResult.FailureResult(msg);
            }
            return dlxValidationResult.SuccessResult;
        }

        /// <summary>
        /// Check Scientific column exist or not
        /// </summary>
        /// <returns>Failure Result or Success result</returns>
        public dlxValidationResult ValidatePriceReport()
        {
            var result =_repository.IsPriceScientificTextExists();
            if (result)
                return dlxValidationResult.FailureResult("There are scientific numbers detected for vendor id in uploaded file. Please correct it to number format and reprocess the file."); 

            return dlxValidationResult.SuccessResult;
        }

        /// <summary>
        /// Check Scientific column exist or not
        /// </summary>
        /// <returns>Failure Result or Success result</returns>
        public dlxValidationResult ValidateQCReport()
        {
            var result = _repository.IsQCScientificTextExists();
            if (result)
                return dlxValidationResult.FailureResult("There are scientific numbers detected for vendor id in uploaded file. Please correct it to number format and reprocess the file."); 

            return dlxValidationResult.SuccessResult;
        }

    }
}
