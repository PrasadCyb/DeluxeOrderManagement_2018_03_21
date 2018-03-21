using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.BulkUploader
{
    /// <summary>
    /// Decide which Ftp configuration to retrive
    /// </summary>
    public enum FtpConfigurationType
    {
        AppleGrid,
        PipelineOrder,
        PriceReport,
        QCReport,
        Announcement,
        VID,
        ProcessTitleReport
    }

    /// <summary>
    /// To populate FTP credentials
    /// </summary>
    public class FtpConfiguration
    {
        public string Host
        {
            get; set;
        }
        public string FtpUserName
        {
            get;
            set;
        }
        public string FtpPassword
        {
            get;
            set;
        }
        public string Provider
        {
            get;
            set;
        }
        public string FtpDirecrory
        {
            get;
            set;
        }

        //public string FtpFilePath
        //{
        //    get;
        //    set;
        //}
        public string FileName
        {
            get;
            set;
        }
        public string DownloadLocalDirectory
        {
            get;
            set;
        }
        public bool KeepOrignal
        {
            get;
            set;
        }
        public bool OverwriteExisting
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public bool EnableSSL
        {
            get;
            set;
        }

        public bool Environment
        {
            get;
            set;
        }
        public string ArchiveDirectory
        {
            get;
            set;
        }


    }
}
