using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DeluxeOM.Utils.Config
{
    public class FtpConfigElement : System.Configuration.ConfigurationElement
    {
        [ConfigurationProperty("fileType")]
        public DownloadFileType? FileType
        {
            get
            {
                return base["fileType"] as DownloadFileType?;
            }
        }

        [ConfigurationProperty("provider", IsKey =true, IsDefaultCollection =true)]
        public Provider? Provider
        {
            get
            {
                return base["provider"] as Provider?;
            }
        }

        [ConfigurationProperty("userName")]
        public string UserName
        {
            get
            {
                return base["userName"] as string;
            }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get
            {
                return base["password"] as string;
            }
        }

        [ConfigurationProperty("ftpLocation")]
        public string FtpLocation
        {
            get
            {
                return base["ftpLocation"] as string;
            }
        }

        [ConfigurationProperty("fileName")]
        public string FileName
        {
            get
            {
                return base["fileName"] as string;
            }
        }

        [ConfigurationProperty("downloadTo")]
        public string DownloadTo
        {
            get
            {
                return base["downloadTo"] as string;
            }
        }

        [ConfigurationProperty("keepOriginal")]
        public bool? KeepOriginal
        {
            get
            {
                return base["keepOriginal"] as bool?;
            }
        }

        [ConfigurationProperty("overwriteExisting")]
        public bool? OverwriteExisting
        {
            get
            {
                return base["overwriteExisting"] as bool?;
            }
        }
        

    }

    public class FtpConfig 
    {
        public DownloadFileType? FileType
        {
            get; set;
        }

        
        public Provider? Provider
        {
            get; set;
        }

        
        public string UserName
        {
            get; set;
        }

        
        public string Password
        {
            get; set;
        }

        
        public string FtpLocation
        {
            get; set;
        }

        
        public string FileName
        {
            get; set;
        }

        
        public string DownloadTo
        {
            get; set;
        }

        public bool? KeepOriginal
        {
            get; set;
        }

        
        public bool? OverwriteExisting
        {
            get; set;
        }


    }

    public class FtpSettings : List<FtpConfigElement>
    {
        public FtpConfigElement this[string provider]
        {
            get
            {
                return this.SingleOrDefault(n => n.Provider.ToString().ToLower() == provider.ToLower());
            }
        }

        public FtpConfigElement this[Provider provider]
        {
            get
            {
                return this.SingleOrDefault(n => n.Provider == provider);
            }
        }
        
    }

    public enum DownloadFileType
    {
        MsExcel,
        Csv
    }

    public enum Provider
    {
        ITuneStore,
        WB,
        Deluxe
    }
}
