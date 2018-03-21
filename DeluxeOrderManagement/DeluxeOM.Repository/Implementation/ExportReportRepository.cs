using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using DeluxeOM.Models;
using System;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Repository
{
    /// <summary>
    /// Methods for export differnet job
    /// </summary>
    public class ExportReportRepository : IExportReportRepository
    {
        string connstring = string.Empty;
        SqlConnection connect;
        SqlCommand cmd;
        string whereClause = string.Empty;
        int JobId = 0;
        /// <summary>
        /// Export Announcement Analysis Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="whereQry">Filters values concatenated into a string</param>
        public void ExportAnnouncementAnalysisReport(string whereQry)
        {
            GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandText = "usp_ExportAnnoucementAnalysisReport";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            #region MOved to Service
            //whereClause = string.Empty;
            //if (!string.IsNullOrEmpty(reportSearch.VideoVersion))
            //{
            //    whereClause = whereClause + " AND  A.VideoVersion= '" + reportSearch.VideoVersion + "'";
            //}
            //if (!string.IsNullOrEmpty(reportSearch.LocalEdit))
            //{
            //    if (string.IsNullOrEmpty(reportSearch.LocalEdit))
            //    {
            //        whereClause = whereClause + " AND A.LocalEdit ='Yes' AND A.LocalEdit ='No'  ";
            //    }
            //    if (reportSearch.LocalEdit.Equals("Yes"))
            //    {
            //        whereClause = whereClause + " AND A.LocalEdit ='Yes'  ";
            //    }
            //    if (reportSearch.LocalEdit.Equals("No"))
            //    {
            //        whereClause = whereClause + "AND A.LocalEdit ='No'  ";
            //    }
            //}
            //if ((reportSearch.CreatedStartDate != null) && !(reportSearch.CreatedStartDate.Equals(DateTime.MinValue)) && (reportSearch.CreatedEndDate != null && !(reportSearch.CreatedEndDate.Equals(DateTime.MinValue))))
            //{
            //    string endDate = reportSearch.CreatedEndDate.AddDays(1).Date.ToString("MM-dd-yyyy");
            //    whereClause = whereClause + " AND A.FirstAnnouncedDate >= '" + reportSearch.CreatedStartDate.ToString("MM-dd-yyyy") + "' and A.FirstAnnouncedDate<= '" + endDate + "' ";
            //}
            ////if ((reportSearch.CreatedEndDate != null && !(reportSearch.CreatedEndDate.Equals(DateTime.MinValue))))
            ////{
            ////    reportSearch.CreatedEndDate = reportSearch.CreatedEndDate.AddDays(1);
            ////    whereClause = whereClause + "AND A.FirstAnnouncedDate >= '" + reportSearch.CreatedStartDate.ToString("MM-dd-yyyy") + "' and A.FirstAnnouncedDate<= '" + reportSearch.CreatedEndDate.ToString("MM-dd-yyyy") + "' " ;
            ////} 
            #endregion

            cmd.Parameters.Add("@whereClause", SqlDbType.VarChar).Value = whereQry;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Export Cancel Avails Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        public void ExportCancelAvailsReport(ReportSearch reportSearch)
        {
            GetConnection();
            cmd = new SqlCommand();
            DataSet dataset = new DataSet();

            cmd.Connection = connect;
            cmd.CommandText = "usp_ExportCancelAvailsReport";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            whereClause = string.Empty;

            if ((reportSearch.CreatedStartDate != null && !(reportSearch.CreatedStartDate.Equals(DateTime.MinValue)) && (reportSearch.CreatedEndDate) != null && !(reportSearch.CreatedEndDate.Equals(DateTime.MinValue))))
            {
                string endDate = reportSearch.CreatedEndDate.AddDays(1).Date.ToString("MM-dd-yyyy");
                whereClause = " and AG.CancellationDate >= '" + reportSearch.CreatedStartDate.ToString("MM-dd-yyyy") + "' and AG.CancellationDate<= '" + endDate + "' ";
            }
            cmd.Parameters.Add("@whereClause", SqlDbType.VarChar).Value = whereClause;
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        /// <summary>
        /// Export Finance Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        public void ExportFinanceReport(ReportSearch reportSearch)
        {
            GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = connect;

            cmd.CommandText = "usp_ExportFinanceReport";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            whereClause = string.Empty;
            if (((reportSearch.ImportStartDate != null && !(reportSearch.ImportStartDate.Equals(DateTime.MinValue))) && ((reportSearch.ImportEndDate) != null) || !(reportSearch.ImportEndDate.Equals(DateTime.MinValue))))
            {
                string endDate = reportSearch.ImportEndDate.AddDays(1).Date.ToString("MM-dd-yyyy");
                whereClause = " and VQ.ImportedDate >= '" + reportSearch.ImportStartDate.ToString("MM-dd-yyyy") + "' and VQ.ImportedDate <= '" + endDate + "' ";
            }

            cmd.Parameters.Add("@whereClause", SqlDbType.VarChar).Value = whereClause;
            cmd.ExecuteNonQuery();
            CloseConnection();
        }


        /// <summary>
        /// Export Announcement Change Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        public void ExportAnnouncementChangeReport(ReportSearch reportSearch)
        {
            GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = connect;

            cmd.CommandText = "usp_ExportAnnouncementChangeReport";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            JobId = 0;
            if (!string.IsNullOrEmpty(reportSearch.JobId))
            {
                JobId = Convert.ToInt32(reportSearch.JobId);
            }

            cmd.Parameters.Add("@JobId", SqlDbType.Int).Value = JobId;
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        /// <summary>
        /// Export Order Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="whrQuery">Filters values concatenated into a string</param>
        /// <param name="channel">channel filter</param>
        public void GenerateOrderReport(string whrQuery, string channel)
        {
            GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandTimeout = 0;
            cmd.CommandText = "usp_Export_Orders";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@whereClause", SqlDbType.VarChar).Value = whrQuery;
            cmd.Parameters.Add("@channel", SqlDbType.VarChar).Value = channel;
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        /// <summary>
        /// Export Title Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="whrQuery">Filters values concatenated into a string</param>
        public void GenerateTitleReport(string whrQuery)
        {
            GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandTimeout = 0;
            cmd.CommandText = "usp_Export_Title";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@whereClause", SqlDbType.VarChar).Value = whrQuery;
            cmd.ExecuteNonQuery();
            CloseConnection();
        }


        /// <summary>
        /// Export VID Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="whrSQLquery">Filters values concatenated into a string</param>
        public void ExportVidReport(string whrSQLquery)
        {
            GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandTimeout = 0;
            cmd.CommandText = "usp_Export_Vid";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@whereClause", SqlDbType.VarChar).Value = whrSQLquery;
            cmd.ExecuteNonQuery();
            CloseConnection();
        }


        /// <summary>
        /// Establish a connection to the database
        /// </summary>
        public void GetConnection()
        {
            connstring = ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString();
            connect = new SqlConnection(connstring);
            connect.Open();

        }

        /// <summary>
        /// Close the current database connection
        /// </summary>
        public void CloseConnection()
        {
            connect.Close();
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var lstCustomers = from c in _context.Customers
                                   where c.Type == (int)Customers.ContentProvider
                                   select c;
                return lstCustomers.ToList();
            }
        }


        /// <summary>
        /// Populate filters value on the job page
        /// </summary>
        /// <returns>Filter Values</returns>
        public ReportSearch GetSearchValue()
        {
            ReportSearch reportsSearch = new ReportSearch();
            var _context = new DeluxeOrderManagementEntities();

            var contentProviders = (from customer in _context.Customers
                                    where customer.Type == (int)Customers.ContentProvider
                                    select customer.Name.ToString()
                               ).Distinct().ToList();

            var contentDistributors = (from customer in _context.Customers
                                       where customer.Type == (int)Customers.ContentDistributor
                                       select customer.Name.ToString()
                                  ).Distinct().ToList();

            var localEdits = new List<string>() { "", "Yes", "No" };

            var announcementProcessedDate = (from job in _context.JOBS
                                             join jobItems in _context.Jobs_Items
                                             on job.Id equals jobItems.JobId
                                             where job.JobType == FtpConfigurationType.Announcement.ToString() &&
                                             job.Status == true
                                             orderby job.Id descending
                                             select new AnnouncementProcessed()
                                             {
                                                 AnnouncemntDate = jobItems.StartDate.Value,
                                                 JobId = jobItems.JobId.ToString(),
                                                 AnnouncementFileName=job.FileName
                                             }
                                           ).ToList().GroupBy(x => x.JobId);
            var announcementProcessed = announcementProcessedDate.Select(x => new AnnouncementProcessed
            {
                AnnouncementFileName=x.Select(m=>m.AnnouncementFileName).FirstOrDefault(),
                JobId= x.Select(m => m.JobId).FirstOrDefault(),
                AnnouncemntDate= x.Select(m => m.AnnouncemntDate).FirstOrDefault(),
            }).ToList();
            reportsSearch.LocalEdits = localEdits;
            reportsSearch.ContentProviders = contentProviders;
            reportsSearch.ContentDistributors = contentDistributors;
            reportsSearch.AnnouncementProcessedDate = announcementProcessed;
            return reportsSearch;
        }


        /// <summary>
        /// Title Record count for selected filter criteria
        /// </summary>
        /// <param name="whrQuery">Filters values concatenated into a string</param>
        /// <returns>Record count</returns>
        public int GetTitleReportRecordCount(string whrQuery)
        {
            int recordCount = 0;

            GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandTimeout = 0;
            cmd.CommandText = "usp_Get_TitleReport_Count";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@whereClause", SqlDbType.VarChar).Value = whrQuery;
            recordCount =  (int)cmd.ExecuteScalar();
            CloseConnection();

            return recordCount;
        }

        /// <summary>
        /// Announcement Analysis Report Record Count for selected filter criteria
        /// </summary>
        /// <param name="whrQuery">Filters values concatenated into a string</param>
        /// <returns>Record count</returns>
        public int GetAnnouncementAnalysisReportRecordCount(string whrQuery)
        {
            int recordCount = 0;

            GetConnection();
            cmd = new SqlCommand();
            cmd.Connection = connect;
            cmd.CommandTimeout = 0;
            cmd.CommandText = "usp_Get_AnnouncementAnalysisReport_Count";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@whereClause", SqlDbType.VarChar).Value = whrQuery;
            recordCount = (int)cmd.ExecuteScalar();
            CloseConnection();

            return recordCount;
        }

    }
}
