using Foolproof;
using System;
using System.Collections.Generic;
using DeluxeOM.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DeluxeOM.Models
{
    public class TitleSearch
    {
        public TitleSearch()
        {

        }
        public string ContentProvider { get; set; }
        public string ContentDistributor { get; set; }

        //[RequiredIfAny("Territory", "Language", "StartDate", "EndDate", ErrorMessage ="Required")]
        public string SelectedTitle { get; set; }

        public string EditType { get; set; }
        public string[] Region { get; set; }
        public string RegionConcat { get; set; }

        //[RequiredIfAny("SelectedTitle", "Language", "StartDate", "EndDate", ErrorMessage = "Required")]
        public string[] Territory { get; set; }
        public string TerritoryConcat{get; set; }

        //[RequiredIfAny("SelectedTitle", "Territory", "StartDate", "EndDate", ErrorMessage = "Required")]
        public string[] Language { get; set; }
        public string LanguageConcate { get; set; }
        public string VideoVersion { get; set; }
        public string MPM { get; set; }
        public DateTime POESTStart { get; set; }
        public DateTime POESTEnd { get; set; }
        
        public DateTime ESTStartDate { get; set; }
        public DateTime ESTEndDate { get; set; }

        //[RequiredIfNotEmpty("EndDate",ErrorMessage ="Start Date is mandatory as End date selected")]
        //[RequiredIfAny("SelectedTitle", "Territory", "Language", "EndDate", ErrorMessage = "Required")]
        [LessThanOrEqualTo("EndDate", ErrorMessage = "From date should be less to To date")]
        public string StartDate { get; set; }
        //[RequiredIfNotEmpty("StartDate", ErrorMessage = "End Date is mandatory as Start date selected")]
        //[RequiredIfAny("SelectedTitle", "Territory", "Language", "StartDate", ErrorMessage = "Required")]
        public string EndDate { get; set; }
        public string VendorId { get; set; }
        public string ComponentType { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public string ChannelDateRange { get; set; }
        public List<string> ContentProviders { get; set; }
        public List<string> ContentDistributors { get; set; }
        public List<string> SelectedTitles { get; set; }
        public List<SelectListItem> SelectedTitleList { get; set; }
        public List<string> EditTypes { get; set; }
        public List<string> Regions { get; set; }
        public List<SelectListItem> RegionList { get; set; }
        public List<string> Territories { get; set; }
        public List<SelectListItem> TerritoryList { get; set; }
        public List<string> Languages { get; set; }
        public List<SelectListItem> LanguageList { get; set; }
        public List<string> VideoVersions { get; set; }

        public List<SelectListItem> VideoVersionList { get; set; }
        public List<string> MPMs { get; set; }
        public List<SelectListItem> MPMList { get; set; }
        public List<string> VendorIds { get; set; }
        public List<SelectListItem> VendorIdList { get; set; }
        public List<string> ComponentTypes { get; set; }

        public List<KeyValue> SortByList { get; set; }
        public List<KeyValue> SortOrderList { get; set; }

        public List<KeyValue> ChannelDateRangeList { get; set; }
    }
}
