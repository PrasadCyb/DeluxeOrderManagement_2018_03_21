using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DeluxeOM.WebUI
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class DeluxeOMAuthorizeAttribute : AuthorizeAttribute
    {
        private char[] separators = { ';', ',', '|' };

        protected virtual ClaimsPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as ClaimsPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthorized = false;

            if (HttpContext.Current.Request.IsAuthenticated && CurrentUser != null)
            {
                // No Specific Roles or Users to authorize
                if (String.IsNullOrEmpty(Roles) && String.IsNullOrEmpty(Users))
                {
                    isAuthorized = true;
                }
                // Check for specific Role to authoize
                if (!isAuthorized && !String.IsNullOrEmpty(Roles))
                {
                    string[] authRoles = Roles.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(ar => ar.Trim()).ToArray();

                    foreach (var role in authRoles)
                    {
                        if (CurrentUser.IsInRole(role))
                        {
                            isAuthorized = true;
                            break;
                        }
                    }
                }

                // Check for specific User to authorize
                if (!isAuthorized && !String.IsNullOrEmpty(Users))
                {
                    string[] authUsers = Users.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(au => au.Trim()).ToArray();

                    if (authUsers.Any(a => a.ToUpper().Equals(CurrentUser.Identity.Name.ToUpper())))
                    {
                        isAuthorized = true;
                    }
                }

            }

            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "SessionTimeout", controller = "Error" }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "AccessDenied", controller = "Error" }));
            }
        }
    }
}