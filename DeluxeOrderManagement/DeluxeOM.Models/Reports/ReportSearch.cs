using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DeluxeOM.Models
{
    public class ReportSearch
    {
        public ReportSearch()
        {

        }
        public string ContentProvider { get; set; }
        public string ContentDistributor { get; set; }
        public string LocalEdit { get; set; }
       
        [Required(ErrorMessage = "Please select start date")]
        [LessThanOrEqualTo("CreatedEndDate",ErrorMessage = "Start date should be less than or equal to end date")]
        public DateTime CreatedStartDate { get; set; }

        [Required(ErrorMessage = "Please select end date")]
        public DateTime CreatedEndDate { get; set; }

        public string VideoVersion { get; set; }
        public List<string> LocalEdits { get; set; }

        public List<string> ContentProviders { get; set; }
        public List<string> ContentDistributors { get; set; }
        public List<string> JobIDs { get; set; }

        public int ReportTitleID { get; set; }

        [Required(ErrorMessage = "Please select start date")]
        [LessThan("ImportEndDate", ErrorMessage = "Start date should be less than end date")]
        public DateTime ImportStartDate { get; set; }


        [Required(ErrorMessage = "Please select start date")]
        public DateTime ImportEndDate { get; set; }

        [Required(ErrorMessage = "Please select Announcement Processed Date")]
        public string JobId { get; set; }
        public List<AnnouncementProcessed> AnnouncementProcessedDate { get; set; }

    }

    public class AnnouncementProcessed
    {
        public DateTime AnnouncemntDate { get; set; }
        public string AnnouncementFileName { get; set; }
        public string Date { get; set; }
        public string JobId { get; set; }
    }
}
