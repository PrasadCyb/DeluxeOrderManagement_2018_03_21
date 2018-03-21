using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DeluxeOM.Utils.Account
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        { }

        

        public string FirstName
        {
            get
            {
                return this.Claims.Where(c => c.Type == ClaimTypes.GivenName)
                   .Select(c => c.Value).SingleOrDefault();
            }
        }

        public string LastName
        {
            get
            {
                return this.Claims.Where(c => c.Type == ClaimTypes.Surname)
                   .Select(c => c.Value).SingleOrDefault();
            }
        }

        public string Username
        {
            get
            {
                return this.Claims.Where(c => c.Type == ClaimTypes.Name )
                   .Select(c => c.Value).SingleOrDefault();
            }
        }

        public string Email
        {
            get
            {
                return this.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();
            }
        }

        public string PhoneNumber
        {
            get
            {
                return this.Claims.Where(c => c.Type == ClaimTypes.MobilePhone)
                   .Select(c => c.Value).SingleOrDefault();
            }
        }

        public IEnumerable<string> Roles
        {
            get
            {
                return this.Claims.Where(c => c.Type == ClaimTypes.Role)
                   .Select(c => c.Value).AsEnumerable<string>();
            }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public int UserId
        {
            get
            {
                return Convert.ToInt32(this.Claims.Where(c => c.Type == ClaimTypes.Sid)
                   .Select(c => c.Value).SingleOrDefault());
            }
        }
    }
}