using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DeluxeOM.Models
{
    public class OrderSearch
    {
        public OrderSearch()
        {

        }
        public string ContentProvider { get; set; }
        public string ContentDistributor { get; set; }
        public string SelectedTitle { get; set; }
        public string EditType { get; set; }

        public string LocalEdit { get; set; }
        public string OrderStatus { get; set; }
        public string MediaType { get; set; }
        public string GreenLightSent { get; set; }
        public string GreenLightReceived { get; set; }
        //public List<ChkValues> LocalEdit { get; set; }
        //public List<ChkValues> OrderStatus { get; set; }
        //public List<ChkValues> MediaType { get; set; }
        //public List<ChkValues> GreenLightSent { get; set; }
        //public List<ChkValues> GreenLightReceived { get; set; }
        public string Region { get; set; }
        public string[] Territory { get; set; }
        public string TerritoryConcat
        {
            get; set;
        }
        public DateTime ESTStartDate { get; set; } 
        public DateTime ESTEndDate { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public string OrderType { get; set; }
        public int pageNumber { get; set; }
        public List<string> ContentProviders { get; set; }
        public List<string> ContentDistributors { get; set; }
        public List<Title> SelectedTitles { get; set; }
        public List<SelectListItem> SelectedTitleList { get; set; }
        public List<string> EditTypes { get; set; }
        public List<string> LocalEdits { get; set; }
        public List<string> OrderStatuses { get; set; }
        public List<string> MediaTypes { get; set; }
        public List<string> GreenLights { get; set; }
        public List<string> Territories { get; set; }
        public List<SelectListItem> TerritoryList { get; set; }
        public List<KeyValue> SortByList { get; set; }
        public List<KeyValue> SortOrderList { get; set; }
        public List<string> OrderTypes { get; set; }

    }
    public class ChkValues
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
