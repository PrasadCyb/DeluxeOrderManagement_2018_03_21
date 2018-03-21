using DeluxeOM.Models;
using DeluxeOM.Repository;
using DeluxeOM.Utils.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace DeluxeOM.Services
{
    public class ExportReportService : IExportReportService
    {
        IExportReportRepository _repository = null;
        string notApplicable = "N/A";
        string blanks = "''";
        /// <summary>
        /// Create instance of ExportReportRepository
        /// </summary>
        public ExportReportService()
        {
            _repository = new ExportReportRepository();
        }
        /// <summary>
        /// Export Announcement Analysis Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        /// <returns>File name and Bytestream</returns>
        public DownLoadFile GenerateAnnouncementAnalysisReport(ReportSearch reportSearch)
        {
            //string annoucementSourceFile = ConfigurationManager.AppSettings["annoucementSourceFile"];
            string annoucementSourceFile = App.Config.AnnouncementReportExportFilePath;
            //string annoucementCopyFile = ConfigurationManager.AppSettings["annoucementCopyFile"];
            string annoucementCopyFile = App.Config.AnnouncementReportExportCopyFilePath;
            string downloadedFileName = "Annoucement Analysis Report" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx";
            //string path = ConfigurationManager.AppSettings["directoryPath"];
            string path = App.Config.ReportDirectoryPath;
            string downloadFile = string.Format(path, downloadedFileName);

            File.Copy(annoucementSourceFile, annoucementCopyFile, true);
            //_repository.ExportAnnouncementAnalysisReport(reportSearch);
            string whereClause = this.getAnnouncementAnalysisReportWhereClause(reportSearch);
            _repository.ExportAnnouncementAnalysisReport(whereClause);

            File.Copy(annoucementSourceFile, downloadFile, true);
            if (File.Exists(annoucementSourceFile))
            {
                File.Delete(annoucementSourceFile);
            }
            File.Copy(annoucementCopyFile, annoucementSourceFile, true);
            if (File.Exists(annoucementCopyFile))
            {
                File.Delete(annoucementCopyFile);
            }
            DownLoadFile download = DownloadFile(downloadFile, downloadedFileName);
            return download;
        }

        /// <summary>
        /// Export Cancel Avails Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        /// <returns>File name and Byte stream</returns>
        public DownLoadFile GenerateCancelAvailsReport(ReportSearch reportSearch)
        {

            //string cancelAvailsSourceFile = ConfigurationManager.AppSettings["cancelAvailsSourceFile"];
            string cancelAvailsSourceFile = App.Config.CancelReportExportFilePath;
            //string cancelAvailsCopyFile = ConfigurationManager.AppSettings["cancelAvailsCopyFile"];
            string cancelAvailsCopyFile = App.Config.CancelReportExportCopyFilePath;
            string downloadedFileName = "Cancel Avails Report" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx";
            //string path = ConfigurationManager.AppSettings["directoryPath"];
            string path = App.Config.ReportDirectoryPath;
            string downloadFile = string.Format(path, downloadedFileName);

            File.Copy(cancelAvailsSourceFile, cancelAvailsCopyFile, true);
            _repository.ExportCancelAvailsReport(reportSearch);

            File.Copy(cancelAvailsSourceFile, downloadFile, true);
            if (File.Exists(cancelAvailsSourceFile))
            {
                File.Delete(cancelAvailsSourceFile);
            }
            File.Copy(cancelAvailsCopyFile, cancelAvailsSourceFile, true);
            if (File.Exists(cancelAvailsCopyFile))
            {
                File.Delete(cancelAvailsCopyFile);
            }
            DownLoadFile download = DownloadFile(downloadFile, downloadedFileName);
            return download;
        }
        /// <summary>
        /// Export Finance Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        /// <returns>File name and Byte stream</returns>
        public DownLoadFile GenerateFinanceReport(ReportSearch reportSearch)
        {
            //string financeSourceFile = ConfigurationManager.AppSettings["financeSourceFile"];

            string financeSourceFile = App.Config.FinanceReportExportFilePath;
            //string financeCopyFile = ConfigurationManager.AppSettings["financeCopyFile"];
            string financeCopyFile = App.Config.FinanceReportExportCopyFilePath;
            string downloadedFileName = "Finance Report" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx";
            //string path = ConfigurationManager.AppSettings["directoryPath"];
            string path = App.Config.ReportDirectoryPath;
            string downloadFile = string.Format(path, downloadedFileName);

            File.Copy(financeSourceFile, financeCopyFile, true);
            _repository.ExportFinanceReport(reportSearch);

            File.Copy(financeSourceFile, downloadFile, true);
            if (File.Exists(financeSourceFile))
            {
                File.Delete(financeSourceFile);
            }
            File.Copy(financeCopyFile, financeSourceFile, true);
            if (File.Exists(financeCopyFile))
            {
                File.Delete(financeCopyFile);
            }
            DownLoadFile download = DownloadFile(downloadFile, downloadedFileName);
            return download;
        }

        /// <summary>
        /// Export Announcement Change Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        /// <returns>File name and Byte stream</returns>
        public DownLoadFile GenerateAnnouncementChangeReport(ReportSearch reportSearch)
        {
            string announcementChangeSourceFile = App.Config.AnnouncementChangeExportFilePath;
            //string financeCopyFile = ConfigurationManager.AppSettings["financeCopyFile"];
            string announcementChangeCopyFile = App.Config.AnnouncementChangeExportCopyFilePath;
            string downloadedFileName = "Announcement Change Report" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx";
            //string path = ConfigurationManager.AppSettings["directoryPath"];
            string path = App.Config.ReportDirectoryPath;
            string downloadFile = string.Format(path, downloadedFileName);

            File.Copy(announcementChangeSourceFile, announcementChangeCopyFile, true);
            _repository.ExportAnnouncementChangeReport(reportSearch);

            File.Copy(announcementChangeSourceFile, downloadFile, true);
            if (File.Exists(announcementChangeSourceFile))
            {
                File.Delete(announcementChangeSourceFile);
            }
            File.Copy(announcementChangeCopyFile, announcementChangeSourceFile, true);
            if (File.Exists(announcementChangeCopyFile))
            {
                File.Delete(announcementChangeCopyFile);
            }
            DownLoadFile download = DownloadFile(downloadFile, downloadedFileName);
            return download;
        }
        /// <summary>
        /// Export Order Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        /// <returns>File name and Byte stream</returns>
        public DownLoadFile GenerateOrderReport(OrderSearch orderSearch)
        {
            string whrSQLquery = string.Empty;
            string channel = string.Empty;
            string temp = string.Empty;
            if (!string.IsNullOrEmpty(orderSearch.StartDate) && !string.IsNullOrEmpty(orderSearch.EndDate))
            {
                //orderSearch.ESTStartDate = Convert.ToDateTime(orderSearch.StartDate);
                //orderSearch.ESTEndDate = Convert.ToDateTime(orderSearch.EndDate);
                orderSearch.ESTStartDate = DateTime.ParseExact(orderSearch.StartDate, "MM/dd/yyyy", null);
                orderSearch.ESTEndDate = DateTime.ParseExact(orderSearch.EndDate, "MM/dd/yyyy", null);
            }
            if (!string.IsNullOrEmpty(orderSearch.SelectedTitle))
            {
                temp = orderSearch.SelectedTitle;
                orderSearch.SelectedTitle = orderSearch.SelectedTitle.Replace("'", "\''");
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " AG.VideoVersion ='" + orderSearch.SelectedTitle + "'  " : whrSQLquery + " AND AG.VideoVersion = '" + orderSearch.SelectedTitle + "' ";
            }
            if (!string.IsNullOrEmpty(orderSearch.EditType))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.Category ='" + orderSearch.EditType + "'  " : whrSQLquery + " AND O.Category = '" + orderSearch.EditType + "' ";
            }
            if (!string.IsNullOrEmpty(orderSearch.LocalEdit))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " AG.LocalEdit ='" + orderSearch.LocalEdit + "'  " : whrSQLquery + " AND AG.LocalEdit = '" + orderSearch.LocalEdit + "' ";
            }
            if (!string.IsNullOrEmpty(orderSearch.OrderStatus))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.OrderStatus ='" + orderSearch.OrderStatus + "'  " : whrSQLquery + " AND O.OrderStatus = '" + orderSearch.OrderStatus + "' ";
            }
            if (!string.IsNullOrEmpty(orderSearch.GreenLightSent))
            {
                if (orderSearch.GreenLightSent.Equals("Yes"))
                {
                    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.GreenlightSenttoPackaging IS NOT NULL " : whrSQLquery + " AND O.GreenlightSenttoPackaging IS NOT NULL ";
                    //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.GreenlightSenttoPackaging IS NOT NULL OR O.GreenlightSenttoPackaging <>'" + blanks + "'" : whrSQLquery + "AND O.GreenlightSenttoPackaging IS NOT NULL OR O.GreenlightSenttoPackaging <>'" + blanks + "'";
                }
                else if (orderSearch.GreenLightSent.Equals("No"))
                {
                    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.GreenlightSenttoPackaging IS NULL " : whrSQLquery + " AND O.GreenlightSenttoPackaging IS NULL ";
                    //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.GreenlightSenttoPackaging IS NULL OR O.GreenlightSenttoPackaging ='" + blanks + "'" : whrSQLquery + "AND O.GreenlightSenttoPackaging IS NULL OR O.GreenlightSenttoPackaging ='" + blanks + "'";
                }
            }
            if (!string.IsNullOrEmpty(orderSearch.GreenLightReceived))
            {
                if (orderSearch.GreenLightReceived.Equals("Yes"))
                {
                    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (O.GreenlightValidatedbyDMDM IS NOT NULL ) " : whrSQLquery + " AND (O.GreenlightValidatedbyDMDM IS NOT NULL ) ";
                }
                else if (orderSearch.GreenLightReceived.Equals("No"))
                {
                    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (O.GreenlightValidatedbyDMDM IS NULL ) " : whrSQLquery + " AND (O.GreenlightValidatedbyDMDM IS NULL ) ";
                }
            }
            if (!string.IsNullOrEmpty(orderSearch.Region))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " R.NAME ='" + orderSearch.Region + "'  " : whrSQLquery + " AND R.NAME = '" + orderSearch.Region + "' ";
            }
            if (orderSearch.Territory != null && orderSearch.Territory.Any())
            {
                string concatterritory = orderSearch.Territory.Aggregate((current, next) => current + "', '" + next);
                concatterritory = string.Format("{0}{1}{0}", "'", concatterritory);

                whrSQLquery += string.IsNullOrEmpty(whrSQLquery) ? string.Empty : " AND ";
                whrSQLquery += " T.WBTerritory IN (" + concatterritory + ") ";
                //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " T.WBTerritory ='" + orderSearch.Territory + "'  " : whrSQLquery + " AND T.WBTerritory = '" + orderSearch.Territory + "' ";
            }
            if ((orderSearch.ESTStartDate != null && !(orderSearch.ESTStartDate.Equals(DateTime.MinValue))) && (orderSearch.ESTEndDate != null && !(orderSearch.ESTEndDate.Equals(DateTime.MinValue))))
            {
                string startDate = orderSearch.ESTStartDate.Date.ToString("yyyy-MM-dd");
                string endDate = orderSearch.ESTEndDate.AddDays(1).Date.ToString("yyyy-MM-dd");
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (CF.MinClientStartDate >='" + startDate + "' AND CF.MaxClientEndDate <'" + endDate + "')  " : whrSQLquery + " AND (CF.MinClientStartDate >='" + startDate + "' AND CF.MaxClientEndDate <'" + endDate + "') ";
            }
            if (!string.IsNullOrEmpty(orderSearch.OrderType))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " O.FileType ='" + orderSearch.OrderType + "'  " : whrSQLquery + " AND O.FileType = '" + orderSearch.OrderType + "' ";
            }
            //if (orderSearch.Territory != null && orderSearch.Territory.Count() != 0)
            //{
            //    string territories = null;
            //    foreach (var x in orderSearch.Territory)
            //    {
            //        territories += "'" + x + "'" + ",";
            //    }
            //    territories = territories.Substring(0, territories.Length - 1);
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " T.WBTerritory IN(" + territories + ")  " : whrSQLquery + " AND T.WBTerritory IN(" + territories + ") ";
            //}
            if (!string.IsNullOrEmpty(orderSearch.MediaType))
            {
                channel = string.IsNullOrEmpty(channel) ? " C.Channel ='" + orderSearch.MediaType + "'  " : whrSQLquery + " AND C.Channel = '" + orderSearch.MediaType + "' ";
            }


            if (!string.IsNullOrEmpty(whrSQLquery))
            {
                whrSQLquery = "where" + whrSQLquery;
            }

            if (!string.IsNullOrEmpty(channel))
            {
                channel = "where" + channel;
            }

            if (!string.IsNullOrEmpty(orderSearch.SortBy))
            {
                whrSQLquery = whrSQLquery + " ORDER BY " + " " + orderSearch.SortBy + " " + orderSearch.SortOrder;
            }

            // this if block is to resore title name which is modified for search title with '
            if (!string.IsNullOrEmpty(temp))
            {
                orderSearch.SelectedTitle = temp;
            }

            //string orderReportSourceFile = ConfigurationManager.AppSettings["OrderReportSourceFile"];
            string orderReportSourceFile = App.Config.OrderReportExportFilePath;
            //string orderReportCopyFile = ConfigurationManager.AppSettings["OrderReportCopyFile"];
            string orderReportCopyFile = App.Config.OrderReportExportCopyFilePath;
            string downloadedFileName = "Order Report" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx";
            //string path = ConfigurationManager.AppSettings["directoryPath"];
            string path = App.Config.ReportDirectoryPath;
            string downloadFile = string.Format(path, downloadedFileName);

            File.Copy(orderReportSourceFile, orderReportCopyFile, true);
            _repository.GenerateOrderReport(whrSQLquery, channel);

            File.Copy(orderReportSourceFile, downloadFile, true);
            if (File.Exists(orderReportSourceFile))
            {
                File.Delete(orderReportSourceFile);
            }
            File.Copy(orderReportCopyFile, orderReportSourceFile, true);
            if (File.Exists(orderReportCopyFile))
            {
                File.Delete(orderReportCopyFile);
            }
            DownLoadFile download = DownloadFile(downloadFile, downloadedFileName);
            return download;

        }

        /// <summary>
        /// Export Title Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        /// <returns>File name and Byte stream</returns>
        public DownLoadFile GenerateTitleReport(TitleSearch titleSearch)
        {
            string whrSQLquery = this.getTitleReportWhereClause(titleSearch);

            #region Moved to GetTitleWhereClause()
            //string temp = string.Empty;
            //if (!string.IsNullOrEmpty(titleSearch.StartDate) && !string.IsNullOrEmpty(titleSearch.EndDate))
            //{
            //    //titleSearch.ESTStartDate = Convert.ToDateTime(titleSearch.StartDate);
            //    //titleSearch.ESTEndDate = Convert.ToDateTime(titleSearch.EndDate);
            //    titleSearch.ESTStartDate = DateTime.ParseExact(titleSearch.StartDate, "MM/dd/yyyy", null);
            //    titleSearch.ESTEndDate = DateTime.ParseExact(titleSearch.EndDate, "MM/dd/yyyy", null);
            //}

            //if (!string.IsNullOrEmpty(titleSearch.SelectedTitle))
            //{
            //    temp = titleSearch.SelectedTitle;
            //    titleSearch.SelectedTitle = titleSearch.SelectedTitle.Replace("'", "\''");
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " TitleName ='" + titleSearch.SelectedTitle + "'  " : whrSQLquery + " AND TitleName = '" + titleSearch.SelectedTitle + "' ";
            //}
            //if (!string.IsNullOrEmpty(titleSearch.EditType))
            //{
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " Category ='" + titleSearch.EditType + "'  " : whrSQLquery + " AND Category = '" + titleSearch.EditType + "' ";
            //}
            //if (!string.IsNullOrEmpty(titleSearch.Territory))
            //{
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " AppleTerritory ='" + titleSearch.Territory + "'  " : whrSQLquery + " AND AppleTerritory = '" + titleSearch.Territory + "' ";
            //}

            //if (!string.IsNullOrEmpty(titleSearch.Language))
            //{
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " LanguageName ='" + titleSearch.Language + "'  " : whrSQLquery + " AND LanguageName = '" + titleSearch.Language + "' ";
            //}

            //if (!string.IsNullOrEmpty(titleSearch.VideoVersion))
            //{
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " VideoVersion ='" + titleSearch.VideoVersion + "'  " : whrSQLquery + " AND VideoVersion = '" + titleSearch.VideoVersion + "' ";
            //}

            //if ((titleSearch.ESTStartDate != null && !(titleSearch.ESTStartDate.Equals(DateTime.MinValue))) && (titleSearch.ESTEndDate != null && !(titleSearch.ESTEndDate.Equals(DateTime.MinValue))))
            //{
            //    string estStartDate = titleSearch.ESTStartDate.Date.ToString("yyyy-MM-dd");
            //    string estEndDate = titleSearch.ESTEndDate.AddDays(1).Date.ToString("yyyy-MM-dd");
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (ESTStartDate >='" + estStartDate + "' AND ESTEndDate <'" + estEndDate + "')  " : whrSQLquery + " AND (ESTStartDate >='" + estStartDate + "' AND ESTEndDate <'" + estEndDate + "') ";
            //}

            //if (!string.IsNullOrEmpty(titleSearch.MPM))
            //{
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " MPM ='" + titleSearch.MPM + "'  " : whrSQLquery + " AND MPM = '" + titleSearch.MPM + "' ";
            //}

            //if (!string.IsNullOrEmpty(titleSearch.VendorId))
            //{
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " VendorId ='" + titleSearch.VendorId + "'  " : whrSQLquery + " AND VendorId = '" + titleSearch.VendorId + "' ";
            //}

            //if (!string.IsNullOrEmpty(titleSearch.ComponentType))
            //{
            //    string componentTypes = string.Empty;
            //    if (titleSearch.ComponentType.Equals("Audio"))
            //    {
            //        componentTypes = "'AUDIO'" + "," + "'FORCED_SUBTITLES'" + "," + "'DUB_CREDIT'" + "," + "'AUDIO_DESCRIPTION'";
            //    }
            //    else if (titleSearch.ComponentType.Equals("Sub"))
            //    {
            //        componentTypes = "'SUBTITLES'" + "," + "'CAPTIONS'";
            //    }
            //    else if (titleSearch.ComponentType.Equals("Video"))
            //    {
            //        componentTypes = "'VIDEO'";
            //    }
            //    whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " ComponentType IN(" + componentTypes + ")" : whrSQLquery + " AND ComponentType IN(" + componentTypes + ") ";
            //}

            //if (!string.IsNullOrEmpty(whrSQLquery))
            //{
            //    whrSQLquery = "where" + whrSQLquery;
            //}

            //if (!string.IsNullOrEmpty(titleSearch.SortBy))
            //{
            //    whrSQLquery = whrSQLquery + " ORDER BY " + " " + titleSearch.SortBy + " " + titleSearch.SortOrder;
            //} 
            //// this if block is to resore title name which is modified for search title with '
            //if (!string.IsNullOrEmpty(temp))
            //{
            //    titleSearch.SelectedTitle = temp;
            //}
            #endregion


            //string TitleReportSourceFile = ConfigurationManager.AppSettings["TitleReportSourceFile"];
            string TitleReportSourceFile = App.Config.TitleReportExportFilePath;
            //string TitleReportCopyFile = ConfigurationManager.AppSettings["TitleReportCopyFile"];
            string TitleReportCopyFile = App.Config.TitleReportExportCopyFilePath;

            string downloadedFileName = "Title Report" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx";
            //string path = ConfigurationManager.AppSettings["directoryPath"];
            string path = App.Config.ReportDirectoryPath;
            string downloadFile = string.Format(path, downloadedFileName);
            File.Copy(TitleReportSourceFile, TitleReportCopyFile, true);
            _repository.GenerateTitleReport(whrSQLquery);

            File.Copy(TitleReportSourceFile, downloadFile, true);
            if (File.Exists(TitleReportSourceFile))
            {
                File.Delete(TitleReportSourceFile);
            }
            File.Copy(TitleReportCopyFile, TitleReportSourceFile, true);
            if (File.Exists(TitleReportCopyFile))
            {
                File.Delete(TitleReportCopyFile);
            }
            DownLoadFile download = DownloadFile(downloadFile, downloadedFileName);
            return download;
        }

        /// <summary>
        /// Export VID Report in .xlsx for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters values</param>
        /// <returns>File name and Byte stream</returns>
        public DownLoadFile ExportVidReport(VIDSearch vidSearch)
        {
            string whrSQLquery = string.Empty;
            if (vidSearch.TitleName != null && vidSearch.TitleName.Any())
            {
                var titleNames = vidSearch.TitleName.Select(x => x.Replace("'", "\''")).ToList();
                string concattitle = titleNames.Aggregate((current, next) => current + "', '" + next);
                concattitle = string.Format("{0}{1}{0}", "'", concattitle);

                whrSQLquery += string.IsNullOrEmpty(whrSQLquery) ? string.Empty : " AND ";
                whrSQLquery += " TitleName IN (" + concattitle + ")";
                //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " T.WBTerritory ='" + orderSearch.Territory + "'  " : whrSQLquery + " AND T.WBTerritory = '" + orderSearch.Territory + "' ";
            }

            if (vidSearch.VideoVersion != null && vidSearch.VideoVersion.Any())
            {
                string videoVersion = vidSearch.VideoVersion.Aggregate((current, next) => current + "', '" + next);
                videoVersion = string.Format("{0}{1}{0}", "'", videoVersion);

                whrSQLquery += string.IsNullOrEmpty(whrSQLquery) ? string.Empty : " AND ";
                whrSQLquery += " VideoVersion IN (" + videoVersion + ")";
                //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " T.WBTerritory ='" + orderSearch.Territory + "'  " : whrSQLquery + " AND T.WBTerritory = '" + orderSearch.Territory + "' ";
            }

            if (vidSearch.VendorId != null && vidSearch.VendorId.Any())
            {
                string vendorId = vidSearch.VendorId.Aggregate((current, next) => current + "', '" + next);
                vendorId = string.Format("{0}{1}{0}", "'", vendorId);

                whrSQLquery += string.IsNullOrEmpty(whrSQLquery) ? string.Empty : " AND ";
                whrSQLquery += " VendorId IN (" + vendorId + ")";
                //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " T.WBTerritory ='" + orderSearch.Territory + "'  " : whrSQLquery + " AND T.WBTerritory = '" + orderSearch.Territory + "' ";
            }

            if (!string.IsNullOrEmpty(vidSearch.VidStatus))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " VIDStatus ='" + vidSearch.VidStatus + "'  " : whrSQLquery + " AND VIDStatus = '" + vidSearch.VidStatus + "' ";
            }

            if (!string.IsNullOrEmpty(whrSQLquery))
            {
                whrSQLquery = "where" + whrSQLquery;
            }
            string VidReportSourceFile = App.Config.VidReportExportFilePath;
            string VidReportCopyFile = App.Config.VidReportExportCopyFilePath;

            string downloadedFileName = "Vid Report" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xlsx";
            string path = App.Config.ReportDirectoryPath;
            string downloadFile = string.Format(path, downloadedFileName);
            File.Copy(VidReportSourceFile, VidReportCopyFile, true);
            _repository.ExportVidReport(whrSQLquery);

            File.Copy(VidReportSourceFile, downloadFile, true);
            if (File.Exists(VidReportSourceFile))
            {
                File.Delete(VidReportSourceFile);
            }
            File.Copy(VidReportCopyFile, VidReportSourceFile, true);
            if (File.Exists(VidReportCopyFile))
            {
                File.Delete(VidReportCopyFile);
            }
            DownLoadFile download = DownloadFile(downloadFile, downloadedFileName);
            return download;
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = _repository.GetCustomers();
            return customers;
        }

        /// <summary>
        /// Populates report values
        /// </summary>
        /// <returns>ReportSearch</returns>
        public ReportSearch GetSearchValues()
        {
            ReportSearch reportsSearch = _repository.GetSearchValue();
            reportsSearch.AnnouncementProcessedDate.ForEach(x =>
            x.Date = ((x.AnnouncemntDate.ToString("dd MMM yyyy")) + " " + (!string.IsNullOrEmpty(x.AnnouncementFileName)? "(" +x.AnnouncementFileName+")" : string.Empty) + " " + string.Format("{0:hh:mm tt}", x.AnnouncemntDate)));
           
            return reportsSearch;
        }
        /// <summary>
        /// Covert .xlsx file to byte stream
        /// </summary>
        /// <param name="file">file path</param>
        /// <param name="downloadedFileName">ile name</param>
        /// <returns>File name and Byte stream</returns>
        public DownLoadFile DownloadFile(string file,string downloadedFileName)
        {
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);

            byte[] buffer = new byte[2048];
            byte[] bufferByte;
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                bufferByte = ms.ToArray();
            };
            stream.Close();
            File.Delete(file);
            DownLoadFile downloadFile = new DownLoadFile() { bufferByte = bufferByte, FileName = downloadedFileName };
            return downloadFile;
        }

        /// <summary>
        /// Concatenate filter value in string
        /// </summary>
        /// <param name="titleSearch">Filters value</param>
        /// <returns>Filters values concatenated into a string</returns>
        private string getTitleReportWhereClause(TitleSearch titleSearch)
        {
            string whrSQLquery = string.Empty;
            string temp = string.Empty;
            if (!string.IsNullOrEmpty(titleSearch.StartDate) && !string.IsNullOrEmpty(titleSearch.EndDate))
            {
                //titleSearch.ESTStartDate = DateTime.ParseExact(titleSearch.StartDate, "MM/dd/yyyy", null);
                //titleSearch.ESTEndDate = DateTime.ParseExact(titleSearch.EndDate, "MM/dd/yyyy", null);
                var channelStart = DateTime.ParseExact(titleSearch.StartDate, "MM/dd/yyyy", null);
                var channelEnd = DateTime.ParseExact(titleSearch.EndDate, "MM/dd/yyyy", null);
                if (titleSearch.ChannelDateRange.ToLower() == "est")
                {
                    titleSearch.ESTStartDate = channelStart;
                    titleSearch.ESTEndDate = channelEnd;
                }
                else if (titleSearch.ChannelDateRange.ToLower() == "poest")
                {
                    titleSearch.POESTStart = channelStart;
                    titleSearch.POESTEnd = channelEnd;
                }
            }

            if (!string.IsNullOrEmpty(titleSearch.SelectedTitle))
            {
                temp = titleSearch.SelectedTitle;
                titleSearch.SelectedTitle = titleSearch.SelectedTitle.Replace("'", "\''");
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " TitleName ='" + titleSearch.SelectedTitle + "'  " : whrSQLquery + " AND TitleName = '" + titleSearch.SelectedTitle + "' ";
            }
            if (!string.IsNullOrEmpty(titleSearch.EditType))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " Category ='" + titleSearch.EditType + "'  " : whrSQLquery + " AND Category = '" + titleSearch.EditType + "' ";
            }
            if (titleSearch.Region != null && titleSearch.Region.Any())
            {
                var concat = titleSearch.Region.Aggregate((current, next) => current + "', '" + next);
                concat = string.Format("{0}{1}{0}", "'", concat);
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " RegionName IN(" + concat + ") " : whrSQLquery + " AND RegionName IN(" + concat + ") ";
            }
            if (titleSearch.Territory != null && titleSearch.Territory.Any())
            {
                var concatterritory = titleSearch.Territory.Aggregate((current, next) => current + "', '" + next);
                concatterritory = string.Format("{0}{1}{0}", "'", concatterritory);
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " AppleTerritory IN(" + concatterritory + ") " : whrSQLquery + " AND AppleTerritory IN(" + concatterritory + ") ";
            }

            if (titleSearch.Language != null && titleSearch.Language.Any())
            {
                var concateLanguage = titleSearch.Language.Aggregate((current, next) => current + "', '" + next);
                concateLanguage = string.Format("{0}{1}{0}", "'", concateLanguage);
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " LanguageName IN(" + concateLanguage + ")  " : whrSQLquery + " AND LanguageName IN(" + concateLanguage + ") ";
            }

            if (!string.IsNullOrEmpty(titleSearch.VideoVersion))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " VideoVersion ='" + titleSearch.VideoVersion + "'  " : whrSQLquery + " AND VideoVersion = '" + titleSearch.VideoVersion + "' ";
            }

            if ((titleSearch.ESTStartDate != null && !(titleSearch.ESTStartDate.Equals(DateTime.MinValue))) && (titleSearch.ESTEndDate != null && !(titleSearch.ESTEndDate.Equals(DateTime.MinValue))))
            {
                string estStartDate = titleSearch.ESTStartDate.Date.ToString("yyyy-MM-dd");
                string estEndDate = titleSearch.ESTEndDate.AddDays(1).Date.ToString("yyyy-MM-dd");
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (ESTStartDate >='" + estStartDate + "' AND ESTStartDate <'" + estEndDate + "')  " : whrSQLquery + " AND (ESTStartDate >='" + estStartDate + "' AND ESTStartDate <'" + estEndDate + "') ";
            }
            if ((titleSearch.POESTStart != null && !(titleSearch.POESTStart.Equals(DateTime.MinValue))) && (titleSearch.POESTEnd != null && !(titleSearch.POESTEnd.Equals(DateTime.MinValue))))
            {
                string poeststart = titleSearch.POESTStart.Date.ToString("yyyy-MM-dd");
                string poestend = titleSearch.POESTEnd.AddDays(1).Date.ToString("yyyy-MM-dd");
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (POESTStartDate >='" + poeststart + "' AND POESTStartDate <'" + poestend + "')  " : whrSQLquery + " AND (POESTStartDate >='" + poeststart + "' AND POESTStartDate <'" + poestend + "') ";
            }
            if (!string.IsNullOrEmpty(titleSearch.MPM))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " MPM ='" + titleSearch.MPM + "'  " : whrSQLquery + " AND MPM = '" + titleSearch.MPM + "' ";
            }

            if (!string.IsNullOrEmpty(titleSearch.VendorId))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " VendorId ='" + titleSearch.VendorId + "'  " : whrSQLquery + " AND VendorId = '" + titleSearch.VendorId + "' ";
            }

            if (!string.IsNullOrEmpty(titleSearch.ComponentType))
            {
                string componentTypes = string.Empty;
                if (titleSearch.ComponentType.Equals("Audio"))
                {
                    componentTypes = "'AUDIO'" + "," + "'FORCED_SUBTITLES'" + "," + "'DUB_CREDIT'" + "," + "'AUDIO_DESCRIPTION'";
                }
                else if (titleSearch.ComponentType.Equals("Sub"))
                {
                    componentTypes = "'SUBTITLES'" + "," + "'CAPTIONS'";
                }
                else if (titleSearch.ComponentType.Equals("Video"))
                {
                    componentTypes = "'VIDEO'";
                }
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " ComponentType IN(" + componentTypes + ")" : whrSQLquery + " AND ComponentType IN(" + componentTypes + ") ";
            }

            if (!string.IsNullOrEmpty(whrSQLquery))
            {
                whrSQLquery = "where" + whrSQLquery;
            }

            if (!string.IsNullOrEmpty(titleSearch.SortBy))
            {
                whrSQLquery = whrSQLquery + " ORDER BY " + " " + titleSearch.SortBy + " " + titleSearch.SortOrder;
            }

            // this if block is to resore title name which is modified for search title with '
            if (!string.IsNullOrEmpty(temp))
            {
                titleSearch.SelectedTitle = temp;
            }

            return whrSQLquery;
        }

        /// <summary>
        /// Concatenate filter value in string
        /// </summary>
        /// <param name="reportSearch">Filters value</param>
        /// <returns>Filters values concatenated into a string</returns>
        private string getAnnouncementAnalysisReportWhereClause(ReportSearch reportSearch)
        {
            string whereClause = string.Empty;
            if (!string.IsNullOrEmpty(reportSearch.VideoVersion))
            {
                whereClause = whereClause + " AND  A.VideoVersion= '" + reportSearch.VideoVersion + "'";
            }
            if (!string.IsNullOrEmpty(reportSearch.LocalEdit))
            {
                if (string.IsNullOrEmpty(reportSearch.LocalEdit))
                {
                    whereClause = whereClause + " AND A.LocalEdit ='Yes' AND A.LocalEdit ='No'  ";
                }
                if (reportSearch.LocalEdit.Equals("Yes"))
                {
                    whereClause = whereClause + " AND A.LocalEdit ='Yes'  ";
                }
                if (reportSearch.LocalEdit.Equals("No"))
                {
                    whereClause = whereClause + "AND A.LocalEdit ='No'  ";
                }
            }
            if ((reportSearch.CreatedStartDate != null) && !(reportSearch.CreatedStartDate.Equals(DateTime.MinValue)) && (reportSearch.CreatedEndDate != null && !(reportSearch.CreatedEndDate.Equals(DateTime.MinValue))))
            {
                string endDate = reportSearch.CreatedEndDate.AddDays(1).Date.ToString("MM-dd-yyyy");
                whereClause = whereClause + " AND A.FirstAnnouncedDate >= '" + reportSearch.CreatedStartDate.ToString("MM-dd-yyyy") + "' and A.FirstAnnouncedDate < '" + endDate + "' ";
            }
            return whereClause;
        }

        /// <summary>
        /// Title Record count for selected filter criteria
        /// </summary>
        /// <param name="titleSearch">Filters value</param>
        /// <returns>Record count</returns>
        public int GetTitleReportRecordCount(TitleSearch titleSearch)
        {
            string whereClause = getTitleReportWhereClause(titleSearch);
            return _repository.GetTitleReportRecordCount(whereClause);
        }

        /// <summary>
        /// Announcement Analysis Report Record Count for selected filter criteria
        /// </summary>
        /// <param name="reportSearch">Filters value</param>
        /// <returns>Record count</returns>
        public int GetAnnouncementAnalysisReportRecordCount(ReportSearch reportSearch)
        {
            string whereClause = getAnnouncementAnalysisReportWhereClause(reportSearch);
            return _repository.GetAnnouncementAnalysisReportRecordCount(whereClause);
        }

    }
}
