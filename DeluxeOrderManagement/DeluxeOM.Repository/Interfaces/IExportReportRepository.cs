using DeluxeOM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Repository
{
    public interface IExportReportRepository
    {
        void ExportAnnouncementAnalysisReport(string whereQry);
        void ExportCancelAvailsReport(ReportSearch reportSearch);
        void ExportFinanceReport(ReportSearch reportSearch);
        void ExportAnnouncementChangeReport(ReportSearch reportSearch);
        void GenerateOrderReport(string whrQuery,string channel);
        void GenerateTitleReport(string whrQuery);
        void ExportVidReport(string whrSQLquery);
        List<Customer> GetCustomers();

        ReportSearch GetSearchValue();
        int GetTitleReportRecordCount(string whrQuery);

        int GetAnnouncementAnalysisReportRecordCount(string whrQuery);
    }
}
