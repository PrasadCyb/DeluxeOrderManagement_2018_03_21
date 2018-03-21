using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Repository
{
    public interface IBulkUploadRepository
    {
        FtpConfiguration GetFtpConfig(FtpConfigurationType configType);

        void BulkInsertVID(string fileName);

        int BulkInsertAnnouncement(string fileName);

        int BulkInsertPipeLineOrder(string fileName);
        int BulkInsertPriceReport(string fileName);
        int BulkInsertQCReport(string fileName);

        void InsertPriceReport();
        void InsertQCReport();
        void InsertPipeLineOrder();

        void ProcessTitleReport();

        int JobLog(JOB job);
        void JobItemLog(Jobs_Items jobItem);

        void UpdateJobLog(JOB job);
        void InsertAnnouncement(int id, DateTime? firstAnnouncedDate);
        void InsertJobAnnouncement(int id);
        bool IsAnnouncementProcessed(string fileName);

        bool IsDependentFileProcessed();

        bool IsPriceScientificTextExists();
        bool IsQCScientificTextExists();
    }
}
