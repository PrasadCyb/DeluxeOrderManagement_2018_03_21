using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using DeluxeOM.Services;
using DeluxeOM.Models;
using System.IO;
using DeluxeOM.Utils.Config;

namespace DeluxeOM.WebUI.Controllers
{
    /// <summary>
    /// Contains methods for generate report in .xlsx
    /// </summary>
    [DeluxeOMAuthorize(Roles = "ReportAdmin")]
    public class ReportController : BaseController
    {

        /// <summary>
        /// Populate all filter value on page load
        /// </summary>
        /// <param name="reportSearch"></param>
        /// <returns></returns>
        public ActionResult Reports(ReportSearch reportSearch)
        {
            if (TempData["ErrorMsg"] != null)
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
            }

            ReportsModel reportModel = new ReportsModel();
            ExportReportService _report = new ExportReportService();
            //reportModel.Name = Session["Name"].ToString();
            var searchValues = _report.GetSearchValues();
            reportModel.ReportSearch.ContentDistributors = searchValues.ContentDistributors;
            reportModel.ReportSearch.ContentProviders = searchValues.ContentProviders;
            reportModel.ReportSearch.LocalEdits = searchValues.LocalEdits;
            reportModel.ReportSearch.AnnouncementProcessedDate = searchValues.AnnouncementProcessedDate;

            return View(reportModel);
        }


        /// <summary>
        /// Exports report in .xlsx file
        /// </summary>
        /// <param name="reportSearch">reportSearch contains which report to export and it filters values</param>
        /// <remark>This method export Announcement analysis report,Finance report,Cancel avail report,Date change report</remark>>
        /// <returns>FileResult which help to download report in browser</returns>
        public ActionResult Reports1(ReportSearch reportSearch)
        {
            string path = string.Empty;
            string downloadFile = string.Empty;
            DownLoadFile downLoadFile=null;

            IExportReportService exportService = new ExportReportService();
            if (reportSearch.ReportTitleID.Equals(1))
            {
                int recordCount = exportService.GetAnnouncementAnalysisReportRecordCount(reportSearch);
                if (recordCount > App.Config.MaxAnnouncementAnalysisDownloadRecordLimit)
                {
                    TempData["ErrorMsg"] = string.Format("There are {0} records that you are trying to download. Record count should not exceed {1} records.", recordCount, App.Config.MaxAnnouncementAnalysisDownloadRecordLimit);
                    return RedirectToAction("Reports", "Report", new { @reportSearch = reportSearch });
                }
                downLoadFile = exportService.GenerateAnnouncementAnalysisReport(reportSearch);
            }

            else if (reportSearch.ReportTitleID.Equals(2))
            {
                downLoadFile = exportService.GenerateCancelAvailsReport(reportSearch);
            }
            else if (reportSearch.ReportTitleID.Equals(3))
            {
                downLoadFile = exportService.GenerateFinanceReport(reportSearch);
            }
            else if (reportSearch.ReportTitleID.Equals(4))
            {
                downLoadFile = exportService.GenerateAnnouncementChangeReport(reportSearch);
            }

            return File(downLoadFile.bufferByte, "application/vnd.ms-excel", downLoadFile.FileName);
        }


        /// <summary>
        /// Now this method is not in use
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Download()
        {
            string file = string.Empty;
            if (!string.IsNullOrEmpty(TempData["fileName"].ToString()))
            {
                file = TempData["fileName"].ToString();
                //string path = ConfigurationManager.AppSettings["directoryPath"];
                string path = App.Config.ReportDirectoryPath;
                string fullPath = string.Format(path, file);
                return File(fullPath, "application/vnd.ms-excel", file);
            }
            else
            {
                return View();
            }
        }
    }
}