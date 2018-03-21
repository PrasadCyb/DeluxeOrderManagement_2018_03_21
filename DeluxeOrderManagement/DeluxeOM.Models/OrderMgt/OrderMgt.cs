using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;


namespace DeluxeOM.Models
{
    public class OrderMgt
    {
        public OrderMgt()
        {
            this.OrderSearch = new OrderSearch();
        }
        public PagedList.IPagedList<Order> orders { get; set; }
        public OrderSearch OrderSearch { get; set; }
        public bool SavedStatus { get; set; }
        public int? UserId { get; set; }
        public int OrderUnlockPeriod { get; set; }
    }
}
