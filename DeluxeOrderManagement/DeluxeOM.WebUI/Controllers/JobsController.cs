using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeluxeOM.Services;
using DeluxeOM.Models.BulkUploader;
using PagedList;
using System.Diagnostics;
using DeluxeOM.Utils;
using System.IO;

namespace DeluxeOM.WebUI.Controllers
{
    /// <summary>
    /// Contains all methods for executing jobs 
    /// </summary>
    [DeluxeOMAuthorize(Roles = "SystemAdmin")]
    public class JobsController : BaseController
    {
        private string logSource = "DeluxeOrderMgt";
        private IJobsService _service = null;
        string tmpMsg = "Message";

        /// <summary>
        /// Create instance for JobsService and source for EventLog
        /// </summary>
        public JobsController()
        {
            if (!EventLog.SourceExists(logSource))
                EventLog.CreateEventSource(logSource, "Application");

            _service = new JobsService();
        }

        /// <summary>
        /// Display all jobs run in the system
        /// </summary>
        /// <param name="pageNumber">pageNumber contains which page to be displayed on the UI</param>
        /// <remark>This method displays all jobs run in the system except successful Process Title Report</remark>
        /// <returns>Index View</returns>
        public ActionResult Index(int? pageNumber)
        {
            var allJobs = _service.GetAll();
            var exceptJobs = allJobs.Where(x => x.JobType == "ProcessTitleReport" && (x.Status.HasValue && x.Status.Value));
            var jobs = allJobs.Except(exceptJobs)
                .ToPagedList(pageNumber ?? 1, 10);
            
            if (TempData[tmpMsg] != null)
                ViewBag.Message = (string)TempData[tmpMsg];

            string selectedItem = string.Empty;
            if (TempData["selectedJobType"] != null)
                selectedItem = (string)TempData["selectedJobType"];
            else
                selectedItem = JobsConstants.SelectedJobKey;
            ViewBag.SelectedItem = getSelectList(selectedItem);

            return View(jobs);
        }

        /// <summary>
        /// Display details of Job
        /// </summary>
        /// <param name="id">Job Id</param>
        /// <returns>Details View</returns>
        public ActionResult Details(int id)
        {
            var job = _service.GetJobsItems(id);
            if (job != null)
            {
                return View(job);
            }

            ViewBag.Message = "No data for job.";
            return View();
        }

        /// <summary>
        /// Execute selected job
        /// </summary>
        /// <param name="jobType">Contains which job to be executed</param>
        /// <param name="announcementDate">First announcement date selected from UI to process announcement file</param>
        /// <param name="file">This parameter consists of a selected .xlsx file through the UI</param>
        /// <remark>This method execute Pipeline Order,Price Report,QC Report,Announcement,Process Title Report</remark>
        /// <returns>Index View</returns>
        [HttpPost]
        public ActionResult Run(string jobType, string announcementDate, HttpPostedFileBase file)
        {
            
            string path = "";
            string fileName = "";
            if (file != null && jobType!="tr")
            {
                fileName = Path.GetFileName(file.FileName);
                path = _service.GetFileUploadPath(jobType, fileName);
                file.SaveAs(path);
            }
            
            EventLog.WriteEntry(logSource, "Jobs controller Run() started. Args: " + jobType + " announcementDate: " + announcementDate);

            if (string.IsNullOrEmpty(announcementDate))
            {
                //announcementDate = DateTime.Now.ToString("MM/dd/yyyy");
                announcementDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            EventLog.WriteEntry(logSource, "JobsController.Run() Announced Date :" + announcementDate);
            //annoucedDate = DateTime.Parse(announcementDate);
            DateTime  annoucedDate = DateTime.ParseExact(announcementDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            
            EventLog.WriteEntry(logSource, "JobsController.Run() Current User :" + this.CurrentUser.FullName); 
            _service.Run(jobType, annoucedDate, this.CurrentUser.FullName, fileName.Replace(' ', '%'));
            //var jobs = _service.GetAll().ToPagedList(1, 10);
            //var jobs = _service.GetAll();

            //TempData["tmpjob"] = jobs;
            TempData["selectedJobType"] = jobType;
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Fetch list of jobs to be executed
        /// </summary>
        /// <param name="selectedVal">Default selected value for dropdown</param>
        /// <returns>SelectedListItems</returns>
        private List<SelectListItem> getSelectList(string selectedVal)
        {
            //var list = new List<SelectListItem>
            //            {
            //                new SelectListItem{ Text="1. Pipeline Order", Value = "po" },
            //                new SelectListItem{ Text="2. Price Report", Value = "pr" },
            //                new SelectListItem{ Text="3. QC Report", Value = "qc" },
            //                new SelectListItem { Text = "4. Announcement", Value = "ann" },
            //                new SelectListItem { Text = "Process Title Report", Value = "tr" }
            //            };

            var list = new List<SelectListItem>();
            list.AddRange(JobsConstants.JobsList.Select(keyValuePair => new SelectListItem()
            {
                Value = keyValuePair.Key,
                Text = keyValuePair.Value
            }));

            list.Where(x => x.Value == selectedVal).FirstOrDefault().Selected = true;

            return list;
        }




    }
}
