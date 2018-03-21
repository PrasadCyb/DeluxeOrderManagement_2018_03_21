using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DeluxeOM.Models
{
    public class VIDSearch
    {
        public VIDSearch()
        {

        }
        public List<string> TitleName { get; set; }
        public List<string> VideoVersion { get; set; }
        public List<string> VendorId { get; set; }
        public string VidStatus { get; set; }
        public List<KeyValue> TitleList { get; set; }
        public List<KeyValue> VideoVersionList { get; set; }
        public List<KeyValue> VendorIdList { get; set; }
        public List<KeyValue> ListVidStatus { get; set; }

    }
}
