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
    
    public partial class Announceemnt_Channel_Format
    {
        public int ID { get; set; }
        public int AnnouncementId { get; set; }
        public string Channel { get; set; }
        public string Format { get; set; }
        public string ClientAvailStatus { get; set; }
        public Nullable<System.DateTime> ClientStartDate { get; set; }
        public int CustomerId { get; set; }
        public Nullable<int> JObId { get; set; }
        public string ChannelFormat { get; set; }
        public Nullable<System.DateTime> ClientEndDate { get; set; }
    }
}
