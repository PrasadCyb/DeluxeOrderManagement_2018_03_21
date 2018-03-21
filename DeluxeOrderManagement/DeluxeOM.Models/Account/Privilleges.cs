using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DeluxeOM.Models.Account
{
    public class Privilleges
    {
        public Privilleges()
        {
            ModifyDatetime = DateTime.UtcNow;
            this.Roles = new List<RoleMembership>();
        }

        public int PrivId { get; set; }

        [Required, StringLength(50)]
        public string PrivName { get; set; }
        public string Description { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public IEnumerable<RoleMembership> Roles { get; set; }
    }
}
