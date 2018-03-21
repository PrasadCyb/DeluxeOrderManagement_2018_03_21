using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DeluxeOM.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{annId}/{vidid}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional,annId=UrlParameter.Optional, vidid = UrlParameter.Optional }
            );
        }
    }
}
