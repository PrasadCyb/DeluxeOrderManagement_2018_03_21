using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class VIDMgt
    {
        public VIDMgt()
        {
            this.VIDSearch = new VIDSearch();
        }
        public VIDModel VID { get; set; }
        public VIDSearch VIDSearch { get; set; }
    }
}
