using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DeluxeOM.Models
{
    public class Order
    {

        public int AnnouncemntId { get; set; }
        public int OrderId { get; set; }
        public int?  VId { get; set; }
        public string Title { get; set; }
        public string OrderStatus { get; set; }
        public string OrderCategory { get; set; }
        public string Region { get; set; }
        public string Territory { get; set; }
        public string Language { get; set; }
        [StringLength(20, ErrorMessage = "You can enter upto 20 character")]
        public string RequestNumber { get; set; }
        [StringLength(20, ErrorMessage = "You can enter upto 20 character")]
        public string MPO { get; set; }
        [StringLength(20, ErrorMessage = "You can enter upto 20 character")]
        public string HALID { get; set; }

        [StringLength(20,ErrorMessage ="You can enter upto 20 character")]
        public string VendorId { get; set; }
        public string LocalEdit { get; set; }
        public string FirstAnnouncementDate { get; set; }
        public string FirstStartDate { get; set; }
        public string DueDate { get; set; }
        public string DeliveryDate { get; set; }
        public string GreenlightReceived { get; set; }
        public string GreenlightSent { get; set; }
        public string AssetRequired { get; set; }
        public string VideoVersion { get; set; }
        public List<ChkValues> Assets { get; set; }
        public string OrderType { get; set; }
        [StringLength(20, ErrorMessage = "You can enter upto 20 character")]
        public string ESTUPC { get; set; }
        [StringLength(20, ErrorMessage = "You can enter upto 20 character")]
        public string VODUPC { get; set; }
        public List<SelectListItem> Territories { get; set; }
        public List<SelectListItem> Languages { get; set; }
        public List<SelectListItem> Titles { get; set; }
        public List<string> LocalEdits { get; set; }
        public List<string> OrderCategories { get; set; }
        public List<string> OrderTypes { get; set; }
        public List<string> OrderStatuses { get; set; }
        public int? LockedBy { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? LockedOn { get; set; }
        public bool IsAlloweToEdit { get; set; }
        public bool IsAllowedToDelete { get; set; }
        public bool IsAnnouncementExist { get; set; } = true;
    }
}
