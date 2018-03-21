using System.Security.Claims;
using System.Web.Mvc;
using DeluxeOM.Utils.Account;

namespace DeluxeOM.WebUI
{
    public abstract class BaseController : Controller
    {
        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }
    }


}