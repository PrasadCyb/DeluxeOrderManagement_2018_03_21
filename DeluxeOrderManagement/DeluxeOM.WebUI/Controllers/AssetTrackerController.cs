using DeluxeOM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeluxeOM.WebUI.Controllers
{
    public class AssetTrackerController : Controller
    {
        private IAssetTrackerService _service = null;
        public AssetTrackerController()
        {
            _service = new AssetTrackerService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AssetTracker()
        {
            return View();
        }
        public ActionResult GetAssetDetails(int iid)
        {
            var assets = _service.GetAsset();
            var jsonResult = new JsonResult { Data = assets, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
            return jsonResult;
        }
    }
}