using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using DeluxeOM.Models;

using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Services
{
    public class FtpFileDownloaderService : IFtpFileDownloaderService
    {
        //FtpFileDownloaderBL _ftpFileDownloaderBL = null;
        public FtpFileDownloaderService()
        {
            //_ftpFileDownloaderBL = new FtpFileDownloaderBL();
        }

        private string UserName { get; set; }
        private string Password { get; set; }

        public void DownloadFile(FtpConfiguration ftpConfig)
        {
            try
            {
                const int BufferSize = 2048;
                byte[] Buffer = new byte[BufferSize];

                FtpWebRequest Request;
                FtpWebResponse Response;
                string filePath = Path.Combine(ftpConfig.DownloadLocalDirectory, ftpConfig.FileName);

                if (File.Exists(filePath))
                {
                    if (ftpConfig.OverwriteExisting)
                    {
                        File.Delete(filePath);
                    }
                    else
                    {
                        //log
                        //Console.WriteLine(string.Format("File {0} already exist.", FileName));
                        return;
                    }
                }

                //Request = (FtpWebRequest)FtpWebRequest.Create(new Uri(Path.Combine(ftpConfig.FtpLocationToWatch, ftpConfig.FileName)));
                Request = (FtpWebRequest)FtpWebRequest.Create(new Uri(Path.Combine(ftpConfig.DownloadLocalDirectory, ftpConfig.FileName)));
                Request.Credentials = new NetworkCredential(UserName, Password);
                Request.Proxy = null;
                Request.Method = WebRequestMethods.Ftp.DownloadFile;
                Request.UseBinary = true;

                Response = (FtpWebResponse)Request.GetResponse();

                using (Stream s = Response.GetResponseStream())
                {
                    //using (FileStream fs = new FileStream(Path.Combine(ftpConfig.DownloadTo, ftpConfig.FileName), FileMode.CreateNew, FileAccess.ReadWrite))
                    using (FileStream fs = new FileStream(Path.Combine(ftpConfig.FtpDirecrory, ftpConfig.FileName), FileMode.CreateNew, FileAccess.ReadWrite))
                    {
                        while (s.Read(Buffer, 0, BufferSize) != -1)
                        {
                            fs.Write(Buffer, 0, BufferSize);
                        }
                    }
                }
            }
            catch { }

        }

        public string[] GetFilesList(string FtpFolderPath)
        {
            try
            {
                FtpWebRequest Request;
                FtpWebResponse Response;

                Request = (FtpWebRequest)FtpWebRequest.Create(new Uri(FtpFolderPath));
                Request.Credentials = new NetworkCredential(UserName, Password);
                Request.Proxy = null;
                Request.Method = WebRequestMethods.Ftp.ListDirectory;
                Request.UseBinary = true;

                Response = (FtpWebResponse)Request.GetResponse();
                StreamReader reader = new StreamReader(Response.GetResponseStream());
                string Data = reader.ReadToEnd();

                return Data.Split('\n');
            }
            catch
            {
                return null;
            }
        }


        public void DeleteFile(string FtpFilePath)
        {
            FtpWebRequest FtpRequest;
            FtpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(FtpFilePath));
            FtpRequest.UseBinary = true;
            FtpRequest.Method = WebRequestMethods.Ftp.DeleteFile;

            FtpRequest.Credentials = new NetworkCredential(UserName, Password);
            FtpWebResponse response = (FtpWebResponse)FtpRequest.GetResponse();
            response.Close();

        }
    }
}
