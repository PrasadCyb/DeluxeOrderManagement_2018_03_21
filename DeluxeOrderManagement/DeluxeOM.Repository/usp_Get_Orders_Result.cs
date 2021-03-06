//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeluxeOM.Repository
{
    using System;
    
    public partial class usp_Get_Orders_Result
    {
        public string OrderStatus { get; set; }
        public string Category { get; set; }
        public string Region { get; set; }
        public string Territory { get; set; }
        public string Language { get; set; }
        public string RequestNumber { get; set; }
        public string MPO { get; set; }
        public string HALID { get; set; }
        public string VendorId { get; set; }
        public int OrderID { get; set; }
        public string Title { get; set; }
        public int AnnouncementId { get; set; }
        public Nullable<System.DateTime> FirstStartDate { get; set; }
        public Nullable<System.DateTime> FirstAnnouncedDate { get; set; }
        public Nullable<System.DateTime> DeliveryDueDate { get; set; }
        public Nullable<System.DateTime> ActualDeliveryDate { get; set; }
        public Nullable<System.DateTime> GreenlightDueDate { get; set; }
        public Nullable<System.DateTime> GreenlightSenttoPackaging { get; set; }
        public Nullable<System.DateTime> GreenlightValidatedbyDMDM { get; set; }
        public string LanguageType { get; set; }
        public string OrderType { get; set; }
        public string ESTUPC { get; set; }
        public string IVODUPC { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public Nullable<System.DateTime> LockedOn { get; set; }
        public Nullable<bool> IsLocked { get; set; }
    }
}
