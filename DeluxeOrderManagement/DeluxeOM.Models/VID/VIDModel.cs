using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class VIDModel
    {
        public int Id { get; set; }
        public string VIDStatus { get; set; }
        public bool Bundle { get; set; }
        public bool Extras { get; set; }
        [Required(ErrorMessage ="Please Enter Title Name")]
        public string TitleName { get; set; }
        public string MPM { get; set; }
        [Required(ErrorMessage = "Please Enter Video Version")]
        [Range(0, Int64.MaxValue, ErrorMessage = "Video Version should not contain characters")]
        public string VideoVersion { get; set; }
        public string EditName { get; set; }
        public string VersionEIDR { get; set; }
        public string AppleId { get; set; }
        public string VendorId { get; set; }
        public string TitleCategory { get; set; }
        public string UserName { get; set; }
        public int JobId { get; set; }
        public List<KeyValue> VidStatusList { get; set; }
        public List<KeyValue> TitleCategoryList { get; set; }
    }
}
