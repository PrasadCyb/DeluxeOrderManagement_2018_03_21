using DeluxeOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Services
{
    public interface IFtpDataService
    {
        bool LogNewFileReceived();
        FtpConfiguration GetPendingFiles();
        string ImpoerFile(string filePath);
    }
}
