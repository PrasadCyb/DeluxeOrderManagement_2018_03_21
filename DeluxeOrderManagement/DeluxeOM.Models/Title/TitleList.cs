using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class TitleList
    {
        public string TitleName { get; set; }
        public string vendorID { get; set; }
        public string VIDType { get; set; }
        public string Region { get; set; }
        public string Territory { get; set; }
        public string Language { get; set; }
        public string LiveStatus { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ApplePOESTStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppleESTStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppleESTEndDate { get; set; }
        public string ESTHDLiveStatus { get; set; }
        public string ESTSDLiveStatus { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppleVODStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppleVODEndDate { get; set; }
        public string VODHDLiveStatus { get; set; }
        public string VODSDLiveStatus { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AnnouncementPOESTStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AnnouncementESTStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AnnouncementESTEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AnnouncementVODStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AnnouncementVODEndDate { get; set; }

        public string ComponentType { get; set; }
        public string ComponentQuality { get; set; }
        public string LanguageType { get; set; }
        public string LanguageStatus { get; set; }

    }
}
