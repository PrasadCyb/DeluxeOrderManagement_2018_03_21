using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeluxeOM.WebUI.Models;

namespace DeluxeOM.WebUI.Controllers
{
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Display Access denied page based on user privileges
        /// </summary>
        /// <returns>Access Denied View</returns>
        public ActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// Display session timeout page after end of session
        /// </summary>
        /// <returns>Session Timeout View</returns>
        public ActionResult SessionTimeout()
        {
            return View();
        }
    }
}