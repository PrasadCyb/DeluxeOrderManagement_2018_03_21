using DeluxeOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Services
{
    public interface IFtpFileDownloaderService
    {
        void DownloadFile(FtpConfiguration ftpConfig);
        string[] GetFilesList(string FtpFolderPath);
        void DeleteFile(string FtpFilePath);
        
    }
}
