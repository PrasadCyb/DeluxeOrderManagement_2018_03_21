using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Models.Common;

namespace DeluxeOM.Services
{
    public interface IBulkUploadService
    {
        FtpConfiguration GetFtpConfig(FtpConfigurationType configType);

        void PopulateVID(string fileName);
        int PopulateAnnouncement(string fileName);
        int PopulatePriceReport(string fileName);
        int PopulateQCReport(string fileName);
        int PopulatePipeLineOrder(string fileName);
        void InsertPriceReport();
        void InsertQCReport();
        void InsertPipeLineOrder();
        void InsertAnnouncement(int id, DateTime? firstAnnouncedDate);
        void InsertJobAnnouncement(int id);
        void ProcessTitleReport();

        int JobLog(Job jobModel);
        void JobItemLog(JobItem jobItemModel);
        void UpdateJobLog(Job jobModel);
        void ProcessNotifications(Models.JobNotificationType jobNotificationType, JobStatus obStatus);

        void SendAnnouncementNewTitlesNotification(JobStatus jobStatus);

        //bool IsAnnouncementProcessed(string fileName);
        dlxValidationResult ValidateAnnouncement(string fileName);
        dlxValidationResult ValidatePriceReport();
        dlxValidationResult ValidateQCReport();
    }
}
