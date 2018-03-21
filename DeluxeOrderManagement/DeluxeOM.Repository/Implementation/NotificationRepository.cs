using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.Common;
using DeluxeOM.Models;

namespace DeluxeOM.Repository
{
    public class NotificationRepository : INotificationRepository
    {

        /// <summary>
        /// Fetch notification from database
        /// </summary>
        /// <param name="notificationType">Type of notification send to users</param>
        /// <returns>Notifications</returns>
        public NotificationEntity GetNotificationMessage(JobNotificationType notificationType)
        {
            string type = string.Empty;
            switch (notificationType)
            {
                case JobNotificationType.LoadAnnouncementNotification:
                    type = "LoadAnnounceemnt";
                    break;
                case JobNotificationType.LoadPriceReportNotification:
                    type = "LoadPriceReport";
                    break;
                case JobNotificationType.LoadQCReportNotification:
                    type = "LoadQCReport";
                    break;
                case JobNotificationType.LoadPipelineOrderNotification:
                    type = "LoadPipelineOrder";
                    break;
                case JobNotificationType.NewTitlesNotification:
                    type = "NewTitles";
                    break;
            }

            using (var _context = new DeluxeOrderManagementEntities())
            {
                var nfnEty = _context.NotificationEntities
                    .FirstOrDefault(x => x.Type == type);

                return nfnEty;
            }
        }


        /// <summary>
        /// Get All notificaion to display on UI
        /// </summary>
        /// <returns>List of notification</returns>
        public List<Notification> GetAllNotification()
        {
            var _context = new DeluxeOrderManagementEntities();
            var notifications = _context.NotificationEntities.Select(x => new Notification()
            {
                ID = x.ID,
                Name = x.Name,
                Type = x.Type,
                Enabled = x.Enabled,
                FromEmailAddress = x.FromEmailAddress,
                ToEmailAddress = x.ToEmailAddress,
                EmailSubject = x.SuccessSubject,
                EmailBody = x.SuccessBody,
                Description = x.Description
            }).ToList();
            return notifications;
        }

        /// <summary>
        /// Update notification in bulk
        /// </summary>
        /// <param name="notifications">Updated list of notifications</param>
        /// <returns>true</returns>
        public bool UpdateNotification(List<Notification> notifications)
        {
            var _context = new DeluxeOrderManagementEntities();
            foreach (var notification in notifications)
            {
                var entity = _context.NotificationEntities.Where(x => x.ID == notification.ID).FirstOrDefault();
                if (entity != null)
                {
                    entity.Enabled = notification.Enabled;
                    entity.ToEmailAddress = notification.ToEmailAddress;
                    _context.SaveChanges();
                }
            }
            return true;
        }

        /// <summary>
        /// Search new title identified into system after a successful import of announcement file
        /// </summary>
        /// <param name="jobId">Announcement jobId</param>
        /// <returns>List of title</returns>
        public List<string> GetNewTitles(int jobId)
        {
            List<string> newTitles = new List<string>();
            var _context = new DeluxeOrderManagementEntities();
            var vids = _context.VIDs.Where(x => x.JobId == jobId);
            if (vids != null && vids.Any())
            {
                newTitles = vids.Select(t => t.TitleName).ToList();
            }
            return newTitles;
        }

        
    }
}
