using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;

namespace DeluxeOM.Repository
{
    public interface INotificationRepository
    {
        NotificationEntity GetNotificationMessage(JobNotificationType notificationType);
        List<Notification> GetAllNotification();

        bool UpdateNotification(List<Notification> notification);

        List<string> GetNewTitles(int jobId);

        //int GetRecordsImportedCount(JobNotificationType notificationType);
    }
}
