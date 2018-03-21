using DeluxeOM.Models;
using DeluxeOM.Services;
using DeluxeOM.Utils.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DeluxeOM.Utils;
using System.Text;

namespace DeluxeOM.WebUI.Controllers
{
    /// <summary>
    /// VID Controller class includes methods for Export, Import and Addition of new VID through the UI
    /// </summary>
    [DeluxeOMAuthorize(Roles = "SystemAdmin")]
    public class VidController : BaseController
    {
        private IVidService _service = null;
        private IExportReportService _export = null;
        ILogger _logger = null;
        /// <summary>
        /// VID Constructor is used to create instance for VidService, ExportReportService and EventLogger
        /// </summary>
        public VidController()
        {
            _service = new VidService();
            _export = new ExportReportService();
            _logger = new EventLogger("VIDController");
        }
        /// <summary>
        /// Populate all dropdown value on VID page and render view
        /// </summary>
        /// <returns>Vid View</returns>
        public ActionResult Vid()
        {
            var vid = new VIDMgt();
            var vidModel = _service.GetDropDownValue();
            var vidSearchValues = _service.GetSearchValue();
            vid.VID = vidModel;
            vid.VIDSearch = vidSearchValues;
            return View(vid);
        }

        /// <summary>
        /// Export VID to .xlsx file
        /// </summary>
        /// <param name="vidSearch">VidSearch contains the filter value to export VID. If vidSearch is null then all VID data get exported</param>
        /// <returns>FileResult which help to download file in browser</returns>
        public ActionResult Export(VIDSearch vidSearch)
        {
            var vid = new VIDMgt();
            var vidSearchValues = _service.GetSearchValue();
            vid.VIDSearch = vidSearchValues;
            DownLoadFile downloadFile = _export.ExportVidReport(vidSearch);
            var vidModel = _service.GetDropDownValue();
            vid.VID = vidModel;
            vid.VIDSearch = vidSearch;
            //return View("Vid", vid);
            string path = App.Config.ReportDirectoryPath;
            return File(downloadFile.bufferByte, "application/vnd.ms-excel", downloadFile.FileName);
        }

        /// <summary>
        /// Import VID list into system
        /// </summary>
        /// <param name="file">This parameter consists of a selected .xlsx file through the UI</param>
        /// <remarks>This method edits VID list in bulk and add new titles in bulk</remarks>
        /// <returns>Vid View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file != null)
            {
                _logger.Info("Import() started. FileName :" + file != null ? file.FileName : string.Empty);
                string result = "Please select valid file !";
                try
                {
                    var supportedTypes = new[] { "xls", "xlsx" };
                    int filesize = 1024 * 5;
                    var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ViewBag.ImportResult = "Only .xls|.xlsx files supported";
                        return RedirectToAction("Vid");
                    }
                    else if (file.ContentLength > (filesize * 1024))
                    {
                        ViewBag.ImportResult = "File size should be upto " + filesize + "KB";
                        return RedirectToAction("Vid");
                    }

                    if (file != null)
                    {
                        string importDirectory = App.Config.ImportVIDDirectory;
                        string fileName = Path.GetFileName(file.FileName);
                        if (!Directory.Exists(importDirectory))
                        {
                            Directory.CreateDirectory(importDirectory);
                        }
                        string filePath = string.Format("{0}/{1}", importDirectory, fileName);
                        file.SaveAs(filePath);

                        _logger.Info(string.Format("File {0} saved", filePath));
                        var messages = _service.Import(filePath, this.CurrentUser.UserId);
                        TempData["ImportResult"] = this.buildValidations(messages);
                        return RedirectToAction("Vid");
                        //return "Operation completed successfully";
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    TempData["ImportResult"] = "There is an error while importing a file.";
                }
            }
            else
            {
                TempData["ImportResult"] = "Please Select File.";
            }

            return RedirectToAction("Vid");
        }


        /// <summary>
        /// Add Title in VID through the UI
        /// </summary>
        /// <param name="vid">This consists of all the required information need to form VID object which is further inserted into the database</param>
        /// <returns>Error or Success results to the UI</returns>
        public ActionResult Add(VIDModel vid)
        {
            var vids = new VIDMgt();
            vid.UserName = CurrentUser.FullName;
            SaveResult result = _service.AddVid(vid);
            var vidModel = _service.GetDropDownValue();
            vids.VID = vidModel;
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View("Vid", vids);
            }
            else
            {
                TempData["VidAddSuccess"] = "VID is added successfully";
            }
            return RedirectToAction("Vid");
        }

        /// <summary>
        /// Format a list of validation into the string
        /// </summary>
        /// <param name="validations">validations contains list of validation(Errors and warning) from database </param>
        /// <returns>Errors and Warning concatenated into the string</returns>
        private string buildValidations(List<ImportValidationResult> validations)
        {
            string result = "File imported successfully.";
            //bool bErr= false;
            if (validations != null && validations.Any())
            {
                bool bErr = false;
                StringBuilder sb = new StringBuilder();
                if (validations.Any(x => x.ValidationLevel == "Error"))
                {
                    bErr = true;
                    sb.Append("Import operation has failed due to following errors. Please correct these errors and re-upload it.");
                    sb.Append("Errors :\n");
                    string errors = string.Join("\n",
                        validations.Where(x => x.ValidationLevel == "Error")
                        .Select(y => string.Format("{0}", y.ValidationMessage)));
                    sb.Append(errors);
                }
                if (validations.Any(x => x.ValidationLevel == "Warning"))
                {
                    sb.Append(bErr ? "" : "Import operation succeeded with below warnings :\n");
                    sb.Append("\n Warnings :\n");
                    string warnings = string.Join("\n",
                        validations.Where(x => x.ValidationLevel == "Warning")
                        .Select(y => string.Format("{0}", y.ValidationMessage)));
                    sb.Append(warnings + "\n");
                }
                result = sb.ToString();
            }
            return result;
        }

    }
}