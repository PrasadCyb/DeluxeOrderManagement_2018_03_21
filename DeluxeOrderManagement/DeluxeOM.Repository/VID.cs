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
    using System.Collections.Generic;
    
    public partial class VID
    {
        public int Id { get; set; }
        public string VIDStatus { get; set; }
        public Nullable<bool> Bundle { get; set; }
        public Nullable<bool> Extras { get; set; }
        public string TitleName { get; set; }
        public string MPM { get; set; }
        public string VideoVersion { get; set; }
        public string EditName { get; set; }
        public string VersionEIDR { get; set; }
        public string AppleId { get; set; }
        public string VendorId { get; set; }
        public string TitleCategory { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> JobId { get; set; }
    }
}
