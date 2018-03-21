using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace DeluxeOM.Models
{
    public class ReportsModel
    {
        public ReportsModel()
        {
            this.ReportSearch = new ReportSearch();
        }

        public ReportSearch ReportSearch { get; set; }

    }
}
