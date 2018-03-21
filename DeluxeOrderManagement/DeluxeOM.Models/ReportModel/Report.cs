using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.Reports
{
    public class ReportModel
    {
        public List<SelectListItem> ProviderList { get; set; }
        public int? ID { get; set; }
        public int? Name { get; set; }
    }
}
