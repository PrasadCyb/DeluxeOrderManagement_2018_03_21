using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DeluxeOM.Repository
{
    public class BulkUploadRepository : IBulkUploadRepository
    {
        /// <summary>
        /// Fetch data for FTP configuation from Database
        /// </summary>
        /// <param name="configType"></param>
        /// <returns>FTPConfiguation</returns>
        public FtpConfiguration GetFtpConfig(FtpConfigurationType configType)
        {
            var _ftpContext = new DeluxeOrderManagementEntities();
            var ftpConfiguration = (from ftpConfig in _ftpContext.FTPConfigs
                     where ftpConfig.FileName == configType.ToString()
                     select new FtpConfiguration { FtpUserName = ftpConfig.UserName,
                                                   FtpPassword = ftpConfig.Password,
                                                   FtpDirecrory = ftpConfig.FtpDirectory,
                                                   Host = ftpConfig.Host,
                                                   DownloadLocalDirectory = ftpConfig.DownloadTo,
                                                   Port = ftpConfig.Port.Value  ,
                                                   EnableSSL = ftpConfig.EnableSSL.Value ,
                                                   ArchiveDirectory = ftpConfig.FtpArchivalDirectory
                     }).FirstOrDefault();
            return ftpConfiguration;
        }

        /// <summary>
        /// Bulk insert VID file into staging table 
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        public void BulkInsertVID(string fileName)
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            _bulkInsertContext.usp_InsertDataToApplePriceStaging(fileName);
        }


        /// <summary>
        /// Bulk insert Announcement file into staging table
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        /// <returns>No record inserted into staging table</returns>
        public int BulkInsertAnnouncement(string fileName)
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            return _bulkInsertContext.usp_InsertDataToAnnouncementStaging(fileName);
        }

        /// <summary>
        /// Bulk insert PipeLine file into staging table
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        /// <returns>No record inserted into staging table</returns>
        public int BulkInsertPipeLineOrder(string fileName)
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            return _bulkInsertContext.usp_InsertDataToPipeLineStaging(fileName);
        }

        /// <summary>
        /// Bulk insert Price Report file into staging table
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        /// <returns>No record inserted into staging table</returns>
        public int BulkInsertPriceReport(string fileName)
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            return _bulkInsertContext.usp_InsertDataToApplePriceStaging(fileName);
        }

        /// <summary>
        /// Bulk insert QC Report file into staging table
        /// </summary>
        /// <param name="fileName">FilePath where it is downloaded</param>
        /// <returns>No record inserted into staging table</returns>
        public int BulkInsertQCReport(string fileName)
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            return _bulkInsertContext.usp_InsertDataToAppleQCStaging(fileName);
        }

        /// <summary>
        /// Process price report into main table
        /// </summary>

        public void InsertPriceReport()
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            _bulkInsertContext.usp_Insert_PriceReport();
        }


        /// <summary>
        /// Process QC report into main table
        /// </summary>
        public void InsertQCReport()
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            _bulkInsertContext.usp_Insert_QCReport();
        }


        /// <summary>
        /// Process Pipeline order into main table
        /// </summary>
        public void InsertPipeLineOrder()
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            _bulkInsertContext.usp_Insert_PipelineOrder();
        }


        /// <summary>
        /// Process announcemnet into main table
        /// </summary>
        /// <param name="id">JonId</param>
        /// <param name="firstAnouncedDate">First announcement date selected from UI</param>
        public void InsertAnnouncement(int id, DateTime? firstAnouncedDate)
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            _bulkInsertContext.usp_Insert_Announcements(id, firstAnouncedDate);
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="id"></param>
        public void InsertJobAnnouncement(int id)
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            //_bulkInsertContext.usp_Insert_Jobs_Announcements(id);
        }


        /// <summary>
        /// Process Title report into TitlesReport table
        /// </summary>
        public void ProcessTitleReport()
        {
            var _bulkInsertContext = new DeluxeOrderManagementEntities();
            _bulkInsertContext.usp_Get_Titles();
        }

        /// <summary>
        /// Log current job into JOB table
        /// </summary>
        /// <param name="job"></param>
        /// <returns>jonID</returns>
        public int JobLog(JOB job)
        {
            var _jobContext = new DeluxeOrderManagementEntities();
            _jobContext.JOBS.Add(job);
            _jobContext.SaveChanges();
            return job.Id;
        }

        /// <summary>
        /// Log corresponding task of job into Jobs_Item table
        /// </summary>
        /// <param name="jobItem"></param>
        public void JobItemLog(Jobs_Items jobItem)
        {
            var _jobContext = new DeluxeOrderManagementEntities();
            _jobContext.Jobs_Items.Add(jobItem);
            _jobContext.SaveChanges();

        }

        /// <summary>
        /// Update current Job
        /// </summary>
        /// <param name="job"></param>
        public void UpdateJobLog(JOB job)
        {
            var _jobContext = new DeluxeOrderManagementEntities();
            var jobUpdate = _jobContext.JOBS.Where(x => x.Id == job.Id).FirstOrDefault();
            jobUpdate.Status = job.Status;
            jobUpdate.Description = job.Description;
            jobUpdate.FileName  = job.FileName ;
            _jobContext.SaveChanges();
        }


        /// <summary>
        /// Check whether announcemnet is already processed or not
        /// </summary>
        /// <param name="fileName">Announcement file name</param>
        /// <returns>True if announcement is already processed else Fasle</returns>
        public bool IsAnnouncementProcessed(string fileName)
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                return _context.JOBS.Any(x => x.FileName == fileName && x.JobType== "Announcement" && (x.Status.HasValue  && x.Status.Value) );
            }
        }

        /// <summary>
        /// Check whether price report and QC Report is processed or not before processing announcement file
        /// </summary>
        /// <returns>True if Price and QC Report file process else False</returns>
        public bool IsDependentFileProcessed()
        {
            bool result  = false ;
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var jobs = _context.JOBS.Where(x => x.JobType == "PriceReport" );
                if (jobs != null && jobs.Any())
                {
                    var status = jobs.OrderByDescending(c => c.Id).First().Status;
                    if (status.HasValue && status.Value)
                        result = true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
                
                jobs = _context.JOBS.Where(x => x.JobType == "QCReport");
                if (jobs != null && jobs.Any())
                {
                    var status = jobs.OrderByDescending(c => c.Id).First().Status;
                    if (status.HasValue && status.Value)
                        result = true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
                
                
                return result ;
            }
        }


        /// <summary>
        /// Check Scientific column exist or not
        /// </summary>
        /// <returns>True if scientific column exist else False</returns>
        public bool IsPriceScientificTextExists()
        {
            //LINQ doesn't fetch corerct value
            //using (var _context = new DeluxeOrderManagementEntities())
            //{
            //    return _context.Database.SqlQuery<bool>("EXECUTE usp_Is_Scientific_Text_Exists reportType", "PriceReport")
            //        .Single<bool>();
            //}
            string constring= ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString();
            SqlConnection connect = new SqlConnection(constring);
            connect.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandText = "usp_Is_Scientific_Text_Exists";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            cmd.Parameters.Add("@ReportType", SqlDbType.VarChar).Value = "PriceReport";
            return (bool)cmd.ExecuteScalar();
        }

        /// <summary>
        /// Check Scientific column exist or not
        /// </summary>
        /// <returns>True if scientific column exist else False</returns>
        public bool IsQCScientificTextExists()
        {
            //LINQ always returns FALSE !!
            //using (var _context = new DeluxeOrderManagementEntities())
            //{
            //    return _context.Database.SqlQuery<bool>("EXECUTE usp_Is_Scientific_Text_Exists reportType", "QCReport")
            //        .Single<bool>();
            //}
            string constring = ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString();
            SqlConnection connect = new SqlConnection(constring);
            connect.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandText = "usp_Is_Scientific_Text_Exists";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            cmd.Parameters.Add("@ReportType", SqlDbType.VarChar).Value = "QCReport";
            return (bool)cmd.ExecuteScalar();
        }

    }
}
