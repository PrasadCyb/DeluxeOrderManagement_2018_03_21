using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public enum FtpFileStatus
    {
        Pending,
        Processed,
        ProcessedWithErrors
    }

    public enum FtpFileProvider
    {
        BroadCast,
        AppleStore,
        WB
    }

    public enum AuthStatus
    {
        None,
        Success,
        InvalidCredentials,
        MaxLoginAttemptsExceeded,
        PasswordExpired,
        PasswordExpiredAllowChange,
        DatabaseError
    }

    //public enum SaveStatus
    //{
    //    Success,
    //    Duplicate,
    //    Error
    //}

    public enum UserSelectionPreference
    {
        UserOnly,
        LastUserPasword,
        NoRoles
    }

    public enum Customers
    {
        ContentProvider = 1,
        ContentDistributor = 2
    }

    public enum JobNotificationType
    {
        LoadAnnouncementNotification,
        LoadPipelineOrderNotification,
        LoadPriceReportNotification,
        LoadQCReportNotification,
        NewTitlesNotification,
        ChangeTitleNotification,
        ProcessTitleReportNotification
    }

    public enum OrderStaus
    {
        New,
        Processing,
        RequestReceived,
        Cancelled,
        Complete,
    }

    public enum JobType
    {
        PriceReport,
        QCReport,
        PipelineOrder,
        Announceemnt,
        TitleReport
    }
}
