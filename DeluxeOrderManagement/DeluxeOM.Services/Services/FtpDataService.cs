using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;

using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Services
{
    public class FtpDataService : IFtpDataService
    {
        //FtpDataBL _ftpDataBL = null;
        public FtpDataService()
        {
            //_ftpDataBL = new FtpDataBL();
        }

        public bool LogNewFileReceived()
        {
            //As sson as Ftp downloading finished, log file path to database so that WinService can process the file into DB
            return true;
        }

        public FtpConfiguration GetPendingFiles()
        {
            //WinService polls db if any files received that awaiting to process
            return new FtpConfiguration();
        }

        public string ImpoerFile(string filePath)
        {
            //WinService will pass downloaded files to process into db
            //bulk upload to dummy table
            //perform calculations and move data fro  dummy to actual table
            //returns if erors occured
            return string.Empty;
        }
    }
}
