using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.Account
{
    public class UserMembership
    {
        public int UserId { get; set; }
        public string Username { get; set; }            // Email Address
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
    }
}
