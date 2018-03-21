using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class Titles
    {
        public Titles()
        {
            this.TitleSearch = new TitleSearch();
        }
        public PagedList.IPagedList<TitleList> TitleList { get; set; }
        public TitleSearch TitleSearch { get; set; }
    }
}
