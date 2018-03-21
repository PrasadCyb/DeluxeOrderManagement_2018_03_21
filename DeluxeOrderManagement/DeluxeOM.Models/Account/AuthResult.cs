using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.Account
{
    /// <summary>
    /// Used in Authunticate user(AccountService)
    /// </summary>
    public class AuthResult
    {
        public AuthStatus Status { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public Object DataObject { get; set; }
    }
}
