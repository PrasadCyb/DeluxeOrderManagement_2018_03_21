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
    
    public partial class AnnouncementGrid
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string VideoVersion { get; set; }
        public string LocalEdit { get; set; }
        public Nullable<int> TerritoryId { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public int CustomerId { get; set; }
        public Nullable<int> JObId { get; set; }
        public Nullable<System.DateTime> FirstAnnouncedDate { get; set; }
        public Nullable<System.DateTime> LastAnnouncedDate { get; set; }
        public Nullable<System.DateTime> CancellationDate { get; set; }
    }
}
