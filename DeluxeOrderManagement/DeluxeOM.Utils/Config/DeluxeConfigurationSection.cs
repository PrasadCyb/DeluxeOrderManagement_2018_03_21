using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DeluxeOM.Utils.Config
{
    
    public class DeluxeConfigurationSection : System.Configuration.ConfigurationSection
    {


        public int MaxLoginAttempts
        {
            get
            {
                return int.Parse(AppSettingsCollection["MaxLoginAttempts"].Value);
            }
        }

        public int PasswordExpiryDays
        {
            get
            {
                return int.Parse(AppSettingsCollection["PasswordExpiryDays"].Value);
            }
        }
        public int OrderUnlockPeriod
        {
            get
            {
                return int.Parse(AppSettingsCollection["OrderUnlockPeriod"].Value);
            }
        }

        public string UIBaseUrl
        {
            get
            {
                return AppSettingsCollection["UIBaseUrl"].Value.ToString();
            }
        }

        public string AdminEmailAddress
        {
            get
            {
                return AppSettingsCollection["AdminEmailAddress"].Value.ToString();
            }

        }
        
        public string RecoverPasswordAction
        {
            get
            {
                return AppSettingsCollection["RecoverPasswordAction"].Value.ToString();
            }
        }

        public int ForgotPasswordInterval
        {
            get
            {
                return int.Parse(AppSettingsCollection["ForgotPasswordInterval"].Value);
            }
        }

        public bool EnableEmailNotifications
        {
            get
            {
                return bool.Parse(AppSettingsCollection["EnableEmailNotifications"].Value);
            }
        }

        public string WCFOrderMgtEndpoint
        {
            get
            {
                return AppSettingsCollection["WCFOrderMgtEndpoint"].Value;
            }
        }

        public string BaseDirectoryPath
        {
            get
            {
                return AppSettingsCollection["BaseDirectoryPath"].Value;
            }
        }

        public string ReportDirectoryPath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["ReportDirectoryPath"].Value) ;
            }
        }

        public string CancelReportExportFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["CancelReportExportFilePath"].Value);
            }
        }

        public string CancelReportExportCopyFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["CancelReportExportCopyFilePath"].Value);
            }
        }

        public string FinanceReportExportFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["FinanceReportExportFilePath"].Value);
            }
        }

        public string FinanceReportExportCopyFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["FinanceReportExportCopyFilePath"].Value);
            }
        }

        public string AnnouncementReportExportFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["AnnouncementReportExportFilePath"].Value);
            }
        }

        public string AnnouncementReportExportCopyFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["AnnouncementReportExportCopyFilePath"].Value);
            }
        }

        public string OrderReportExportFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["OrderReportExportFilePath"].Value);
            }
        }

        public string OrderReportExportCopyFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["OrderReportExportCopyFilePath"].Value);
            }
        }

        public string TitleReportExportFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["TitleReportExportFilePath"].Value);
            }
        }

        public string TitleReportExportCopyFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["TitleReportExportCopyFilePath"].Value);
            }
        }

        public string VidReportExportFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["VidReportExportFilePath"].Value);
            }
        }

        public string VidReportExportCopyFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["VidReportExportCopyFilePath"].Value);
            }
        }

        public string AnnouncementChangeExportFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["AnnouncementChangeExportFilePath"].Value);
            }
        }
        public string AnnouncementChangeExportCopyFilePath
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["AnnouncementChangeExportCopyFilePath"].Value);
            }
        }

        public int MaxTitleReportDownloadRecordLimit
        {
            get
            {
                return int.Parse(AppSettingsCollection["MaxTitleReportDownloadRecordLimit"].Value);
            }
        }

        public int MaxAnnouncementAnalysisDownloadRecordLimit
        {
            get
            {
                return int.Parse(AppSettingsCollection["MaxAnnouncementAnalysisDownloadRecordLimit"].Value);
            }
        }

        public string ImportOrderDirectory
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["ImportOrderDirectory"].Value);
            }
        }

        public string ImportVIDDirectory
        {
            get
            {
                return string.Format("{0}\\{1}", this.BaseDirectoryPath, AppSettingsCollection["ImportVIDDirectory"].Value);
            }
        }






        [ConfigurationProperty("smtpMail", IsRequired = true)]
        public SmtpMailConfigElement SmtpMail
        {
            get
            {
                return base["smtpMail"] as SmtpMailConfigElement;
            }
        }

        [ConfigurationProperty("ftpSettings", IsRequired = true)]
        public FtpSettingsConfigCollection FtpSettingsCollection
        {
            get
            {
                return base["ftpSettings"] as FtpSettingsConfigCollection;
            }
        }

        public FtpSettings FtpSettings
        {
            get
            {
                //return this.FtpSettingsCollection.As<FtpSettings>();
                FtpSettings settings = new FtpSettings();
                foreach (var itm in this.FtpSettingsCollection)
                {
                    settings.Add(itm);
                }
                return settings;
            }
        }

        [ConfigurationProperty("deluxeAppSettings", IsRequired = false)]
        public DeluxeAppSettingsConfigCollection AppSettingsCollection
        {
            get
            {
                return base["deluxeAppSettings"] as DeluxeAppSettingsConfigCollection;
            }
        }

        public DeluxeAppSettings Settings
        {
            get
            {
                DeluxeAppSettings settings = new DeluxeAppSettings();
                foreach (var itm in this.AppSettingsCollection)
                {
                    settings.Add(itm);
                }
                return settings;
            }
        }


    }
}
