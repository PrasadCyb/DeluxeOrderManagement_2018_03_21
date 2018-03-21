using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.Common;
using DeluxeOM.Models;
using DeluxeOM.Repository;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Services
{
    public interface INotificationService
    {
        void SendJobNotification(JobNotificationType notificationTYpe, JobStatus jobStatus);

        void SendEmail(dlxMailMessage message);

        List<Notification> GetAllNotification();

        bool UpdateNotification(List<Notification> notification);

        void SendAnnouncementNewTitlesNotification(JobStatus jobStatus);


    }
}
