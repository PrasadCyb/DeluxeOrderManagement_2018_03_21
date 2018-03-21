using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeluxeOM.WebUI.Controllers
{
    //[DeluxeOMAuthorize(Roles = "ReportAdmin")]
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Order()
        {
            ViewBag.Message = "Order page";

            return View();
        }
        public ActionResult Title()
        {
            ViewBag.Message = "title page";

            return View();
        }
        public ActionResult Report()
        {
            ViewBag.Message = "report page";

            return View();
        }
    }
}