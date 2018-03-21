using DeluxeOM.Models;
using DeluxeOM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Services
{
    public interface IExportReportService
    {
        DownLoadFile GenerateCancelAvailsReport(ReportSearch reportSearch);
        DownLoadFile GenerateFinanceReport(ReportSearch reportSearch);
        DownLoadFile GenerateAnnouncementAnalysisReport(ReportSearch reportSearch);
        DownLoadFile GenerateOrderReport(OrderSearch searchOrder);
        List<Customer> GetCustomers();
        DownLoadFile GenerateAnnouncementChangeReport(ReportSearch reportSearch);
        ReportSearch GetSearchValues();
        DownLoadFile GenerateTitleReport(TitleSearch titleSearch);
        DownLoadFile ExportVidReport(VIDSearch vidSearch);

        int GetTitleReportRecordCount(TitleSearch titleSearch);

        int GetAnnouncementAnalysisReportRecordCount(ReportSearch reportSearch);
    }
}
