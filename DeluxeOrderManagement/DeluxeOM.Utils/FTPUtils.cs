using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;
using System.Net;
using System.IO;

namespace DeluxeOM.Utils
{
    public class FTPUtils
    {
        FtpConfiguration _config;

        public FTPUtils(FtpConfiguration config)
        {
            _config = config;
        }

        public string DownloadFileName
        {
            get; set;
        }
        /// <summary>
        /// Download files(Announcement, Pipeline order, price report, QC Report) from FTP depends upon _config
        /// </summary>
        public void DownloadFile()     //string folderName
        {
                string path =  @"c:\Files";
                
                string filePath = Path.Combine(path, "Apple iTunes Features 2017-08-01");
                //string filePath = Path.Combine(_config.DownloadLocalDirectory, "Apple iTunes Features 2017-08-01");

                //string ftpFilePath = "WB-Apple-Grid/" + folderName + "/";
                string ftpFilePath = _config.FtpDirecrory + "/";

                if (File.Exists(filePath))
                {
                    //if (ftpConfig.OverwriteExisting)
                    //{
                    //    File.Delete(filePath);
                    //}
                    //else
                    //{
                    //    //log
                    //    //Console.WriteLine(string.Format("File {0} already exist.", FileName));
                    //    return;
                    //}
                }
                List<string> listOfFile = GetAllFiles(ftpFilePath);
                if (listOfFile != null && listOfFile.Count > 0)
                {
                    string latestFileName = GetLastModifiedFileName(listOfFile, ftpFilePath);
                    if (latestFileName != null)
                    {
                        System.Net.FtpWebRequest ftpRequest = (System.Net.FtpWebRequest)WebRequest.Create(_config.Host +"/" + ftpFilePath + latestFileName);
                        ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;

                        // FTP Login
                        //ftpRequest.Credentials = new NetworkCredential("prasadja", "4rfv$RFV");
                        ftpRequest.Credentials = new NetworkCredential(_config.FtpUserName, _config.FtpPassword);

                        ftpRequest.EnableSsl = _config.EnableSSL;
                        int bufferSize = 2048;
                        int readCount;
                        byte[] buffer = new byte[bufferSize];
                        using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                        {
                            using (Stream responseStream = ftpResponse.GetResponseStream())
                            {
                            //@"D:/DeluxeOrderManagement/FTPDownloadPath/"
                            readCount = responseStream.Read(buffer, 0, bufferSize);
                                if (!Directory.Exists(_config.DownloadLocalDirectory + ftpFilePath))
                                    Directory.CreateDirectory(_config.DownloadLocalDirectory + ftpFilePath);
                            //if (!Directory.Exists(@"//172.27.183.207/FTPDownloadPath" + ftpFilePath))
                            //    Directory.CreateDirectory(@"//172.27.183.207/FTPDownloadPath" + ftpFilePath);
                            //using (FileStream outputStream = new FileStream(@"//172.27.183.207/FTPDownloadPath" + ftpFilePath + latestFileName, FileMode.Create, FileAccess.ReadWrite))
                            using (FileStream outputStream = new FileStream(_config.DownloadLocalDirectory + ftpFilePath + latestFileName, FileMode.Create, FileAccess.ReadWrite))
                                {
                                    while (readCount > 0)
                                    {
                                        outputStream.Write(buffer, 0, readCount);
                                        readCount = responseStream.Read(buffer, 0, bufferSize);
                                    }
                                }
                            }
                        }
                        //DownloadFileName = @"//172.27.183.207/FTPDownloadPath" + ftpFilePath + latestFileName;
                        DownloadFileName = _config.DownloadLocalDirectory + ftpFilePath + latestFileName;
                }
                }
        }

        /// <summary>
        /// Archive processed file from FTP to the new archive directory in FTP
        /// </summary>
        /// <param name="ftpFileName">Contains the name of the archive file</param>
        /// <returns>Successful archive return true else return false</returns>
        public bool Archive(string ftpFileName)
        {
            bool status = false;
            string sourceFtpDir = _config.FtpDirecrory + "/";
            try
            {
                #region Commented
                ////List<string> listOfFile = GetAllFiles(sourceFtpDir);
                ////if (listOfFile != null && listOfFile.Count > 0)
                ////{
                ////    string latestFileName = GetLastModifiedFileName(listOfFile, sourceFtpDir);
                ////    if (latestFileName != null)
                ////    {
                //        FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_config.Host + "/" + _config.FtpDirecrory + "/" + ftpFileName));
                //        reqFTP.UseBinary = true;
                //        reqFTP.Credentials = new NetworkCredential(_config.FtpUserName, _config.FtpPassword);
                //        reqFTP.Method = WebRequestMethods.Ftp.Rename;
                //        reqFTP.EnableSsl = _config.EnableSSL;
                //        reqFTP.RenameTo = _config.ArchiveDirectory + "/" + ftpFileName;
                //        //ftp.RenameTo = Uri.UnescapeDataString(targetUriRelative.OriginalString);
                //        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                //        status = true;
                ////    }
                ////}
                //////return response.StatusCode == FtpStatusCode.; 

                //perform rename
                //FtpWebRequest ftp = GetRequest(uriSource.AbsoluteUri);
                #endregion

                Uri uriSource = new Uri(_config.Host + "/" + _config.FtpDirecrory + "/" + ftpFileName, System.UriKind.Absolute);
                Uri uriDestination = new Uri(_config.Host + "/" + _config.ArchiveDirectory + "/" + ftpFileName, System.UriKind.Absolute);

                if (IsFileExists(uriDestination.OriginalString))
                {
                    //if file already archived delete file from archived
                    DeleteFile(uriDestination.OriginalString);
                }

                Uri targetUriRelative = uriSource.MakeRelativeUri(uriDestination);
                System.Net.FtpWebRequest ftp = (System.Net.FtpWebRequest)WebRequest.Create(uriSource.AbsoluteUri);
                ftp.Method = WebRequestMethods.Ftp.Rename;
                ftp.Credentials = new NetworkCredential(_config.FtpUserName, _config.FtpPassword);
                ftp.EnableSsl = _config.EnableSSL;
                
                ftp.RenameTo = Uri.UnescapeDataString(targetUriRelative.OriginalString);

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();

                status = true;
            }
            catch (Exception )
            {
                status = false ;
                throw;
            }
            return status;
        }

        /// <summary>
        /// Find all files in a given FTP directory
        /// </summary>
        /// <param name="ftpFilePath">FTP directory name</param>
        /// <returns>List of file in given FTP directory</returns>
        private List<string> GetAllFiles(string ftpFilePath)
        {

            FtpWebRequest reqFTP;
            WebResponse response = null;
            StreamReader reader = null;
            StringBuilder result = new StringBuilder();
            List<string> listOfFile = new List<string>();
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_config.Host+ "/" + ftpFilePath));
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(_config.FtpUserName, _config.FtpPassword);
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            reqFTP.EnableSsl = _config.EnableSSL;
            response = reqFTP.GetResponse();
            reader = new StreamReader(response.GetResponseStream());
            string line = reader.ReadLine();
            while (line != null)
            {
                listOfFile.Add(line);
                line = reader.ReadLine();
            }
            return listOfFile;
        }
        /// <summary>
        /// Find latest updated files in a given FTP directory
        /// </summary>
        /// <param name="listOfFile">FTP directory name</param>
        /// <param name="ftpFilePath">List of file in given FTP directory</param>
        /// <returns>Latest File name</returns>
        private string GetLastModifiedFileName(List<string> listOfFile, string ftpFilePath)
        {
                string file = null;
                List<FileModel> fileModel = new List<FileModel>();

                foreach (string fileName in listOfFile)
                {
                    FtpWebRequest reqFTP;
                    StringBuilder result = new StringBuilder();
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_config.Host+ "/" + ftpFilePath + fileName));
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(_config.FtpUserName, _config.FtpPassword);
                    reqFTP.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                    reqFTP.EnableSsl = _config.EnableSSL;
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    FileModel fileData = new FileModel() { FileName = fileName, UploadDate = response.LastModified };
                    fileModel.Add(fileData);
                }

                if (fileModel != null || fileModel.Count > 0)
                {
                    file = fileModel.OrderByDescending(x => x.UploadDate).Select(x => x.FileName).FirstOrDefault();
                }
            return file;
        }


        /// <summary>
        /// Check the file to archive is already present in the archive directory
        /// </summary>
        /// <param name="ftpFilePath">FileName</param>
        /// <returns>True/False depends on given file file present or not</returns>
        private bool IsFileExists(string ftpFilePath)
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpFilePath);
            request.Credentials = new NetworkCredential(_config.FtpUserName, _config.FtpPassword);
            request.EnableSsl = _config.EnableSSL;
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }

        /// <summary>
        /// Delete file from archive directory
        /// </summary>
        /// <param name="ftpFilePath">File path</param>
        private void DeleteFile(string ftpFilePath)
        {
            var request = (FtpWebRequest)WebRequest.Create(ftpFilePath);
            request.Credentials = new NetworkCredential(_config.FtpUserName, _config.FtpPassword);
            request.EnableSsl = _config.EnableSSL;
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

        }

    }
}
