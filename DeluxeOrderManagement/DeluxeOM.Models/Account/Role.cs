using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.Account
{
    public class Role
    {
        public Role()
        {
            ModifyDatetime = DateTime.UtcNow;
            this.Users = new List<UserMembership>();
            this.Privs = new List<Privilleges>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime ModifyDatetime { get; set; }
        public IEnumerable<UserMembership> Users { get; set; }
        public IEnumerable<Privilleges> Privs { get; set; }
    }
}
