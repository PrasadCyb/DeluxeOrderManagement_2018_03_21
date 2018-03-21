using DeluxeOM.Models;
using DeluxeOM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DeluxeOM.Utils.Config;
using System.Configuration;

namespace DeluxeOM.WebUI.Controllers
{
    /// <summary>
    /// Contains methods for search and export Title
    /// </summary>
    [DeluxeOMAuthorize(Roles = "ReportAdmin")]
    public class TitleController : BaseController
    {
        // GET: Title
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Populate all filter values on page load
        /// </summary>
        /// <param name="titleSearch"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public ActionResult Titles(TitleSearch titleSearch, int? pageNumber)
        {
            try
            {
                if (TempData["ErrorMsg"] != null)
                {
                    ViewBag.ErrorMsg = TempData["ErrorMsg"];
                }

                TitleService _title = new TitleService();
                var titles = _title.GetTitles().ToPagedList(pageNumber ?? 1, 10);
                var searchValues = _title.GetSearchValues();
                searchValues.SelectedTitleList = searchValues.SelectedTitles.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.TerritoryList = searchValues.Territories.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.LanguageList = searchValues.Languages.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.MPMList = searchValues.MPMs.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.VendorIdList = searchValues.VendorIds.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.VideoVersionList = searchValues.VideoVersions.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.RegionList = searchValues.Regions.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();

                Titles title = new Titles();
                title.TitleSearch = searchValues;
                title.TitleList = titles;
                return View(title);
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Search title on basis of selected filter criteria
        /// </summary>
        /// <param name="titleSearch">titleSearch contains filter values to search titles from a database</param>
        /// <param name="pageNumber"></param>
        /// <returns>Titles View</returns>
        [HttpGet]
        public ActionResult SearchTitle(TitleSearch titleSearch, int? pageNumber)
        {
            #region old code
            //if (!ModelState.IsValid)
            //{
            //    TitleService _title = new TitleService();
            //    var titles = new List<TitleList>().ToPagedList(1, 10);
            //    var searchValues = _title.GetSearchValues();
            //    titleSearch.Territory = string.IsNullOrEmpty(titleSearch.TerritoryConcat) ? titleSearch.Territory : titleSearch.TerritoryConcat.Split('|');
            //    titleSearch.Language = string.IsNullOrEmpty(titleSearch.LanguageConcate) ? titleSearch.Language : titleSearch.LanguageConcate.Split('|');
            //    titleSearch.Region = string.IsNullOrEmpty(titleSearch.RegionConcat) ? titleSearch.Region : titleSearch.RegionConcat.Split('|');
            //    searchValues.SelectedTitleList = searchValues.SelectedTitles.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.TerritoryList = searchValues.Territories.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.LanguageList = searchValues.Languages.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.MPMList = searchValues.MPMs.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.VendorIdList = searchValues.VendorIds.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.VideoVersionList = searchValues.VideoVersions.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.RegionList = searchValues.Regions.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    Titles title = new Titles();
            //    title.TitleList = titles;
            //    title.TitleSearch = titleSearch;
            //    title.TitleSearch.ContentDistributors = searchValues.ContentDistributors;
            //    title.TitleSearch.ContentProviders = searchValues.ContentProviders;
            //    title.TitleSearch.SelectedTitleList = searchValues.SelectedTitleList;
            //    title.TitleSearch.EditTypes = searchValues.EditTypes;
            //    title.TitleSearch.TerritoryList = searchValues.TerritoryList;
            //    title.TitleSearch.LanguageList = searchValues.LanguageList;
            //    title.TitleSearch.VideoVersionList = searchValues.VideoVersionList;
            //    title.TitleSearch.MPMList = searchValues.MPMList;
            //    title.TitleSearch.VendorIdList = searchValues.VendorIdList;
            //    title.TitleSearch.ComponentTypes = searchValues.ComponentTypes;
            //    title.TitleSearch.SortOrderList = searchValues.SortOrderList;
            //    title.TitleSearch.SortByList = searchValues.SortByList;
            //    title.TitleSearch.ChannelDateRangeList = searchValues.ChannelDateRangeList;
            //    title.TitleSearch.RegionList = searchValues.RegionList;
            //    return View("Titles", title);
            //}
            //try
            //{
            //    TitleService _title = new TitleService();
            //    titleSearch.Territory = string.IsNullOrEmpty(titleSearch.TerritoryConcat) ? titleSearch.Territory : titleSearch.TerritoryConcat.Split('|');
            //    titleSearch.Language = string.IsNullOrEmpty(titleSearch.LanguageConcate) ? titleSearch.Language : titleSearch.LanguageConcate.Split('|');
            //    titleSearch.Region = string.IsNullOrEmpty(titleSearch.RegionConcat) ? titleSearch.Region : titleSearch.RegionConcat.Split('|');
            //    var titles = _title.SearchTitles(titleSearch).ToPagedList(pageNumber ?? 1, 10);
            //    var searchValues = _title.GetSearchValues();

            //    searchValues.SelectedTitleList = searchValues.SelectedTitles.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.TerritoryList = searchValues.Territories.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.LanguageList = searchValues.Languages.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.MPMList = searchValues.MPMs.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.VendorIdList = searchValues.VendorIds.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.VideoVersionList = searchValues.VideoVersions.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    searchValues.RegionList = searchValues.Regions.Select(x => new SelectListItem()
            //    {
            //        Text = x,
            //        Value = x
            //    }).Distinct().ToList();
            //    Titles title = new Titles();
            //    title.TitleList = titles;
            //    title.TitleSearch = titleSearch;
            //    title.TitleSearch.ContentDistributors = searchValues.ContentDistributors;
            //    title.TitleSearch.ContentProviders = searchValues.ContentProviders;
            //    title.TitleSearch.SelectedTitleList = searchValues.SelectedTitleList;
            //    title.TitleSearch.EditTypes = searchValues.EditTypes;
            //    title.TitleSearch.TerritoryList = searchValues.TerritoryList;
            //    title.TitleSearch.LanguageList = searchValues.LanguageList;
            //    title.TitleSearch.VideoVersionList = searchValues.VideoVersionList;
            //    title.TitleSearch.MPMList = searchValues.MPMList;
            //    title.TitleSearch.VendorIdList = searchValues.VendorIdList;
            //    title.TitleSearch.ComponentTypes = searchValues.ComponentTypes;
            //    title.TitleSearch.SortOrderList = searchValues.SortOrderList;
            //    title.TitleSearch.SortByList = searchValues.SortByList;
            //    title.TitleSearch.ChannelDateRangeList = searchValues.ChannelDateRangeList;
            //    title.TitleSearch.RegionList = searchValues.RegionList;
            //    return View("Titles", title);
            //}
            //catch (Exception ex)
            //{
            //    return View("Error");
            //} 
            #endregion


            TitleService _title = new TitleService();
            try
            {
                titleSearch.Territory = string.IsNullOrEmpty(titleSearch.TerritoryConcat) ? titleSearch.Territory : titleSearch.TerritoryConcat.Split('|');
                titleSearch.Language = string.IsNullOrEmpty(titleSearch.LanguageConcate) ? titleSearch.Language : titleSearch.LanguageConcate.Split('|');
                titleSearch.Region = string.IsNullOrEmpty(titleSearch.RegionConcat) ? titleSearch.Region : titleSearch.RegionConcat.Split('|');
                var searchValues = _title.GetSearchValues();

                searchValues.SelectedTitleList = searchValues.SelectedTitles.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.TerritoryList = searchValues.Territories.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.LanguageList = searchValues.Languages.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.MPMList = searchValues.MPMs.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.VendorIdList = searchValues.VendorIds.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.VideoVersionList = searchValues.VideoVersions.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                searchValues.RegionList = searchValues.Regions.Select(x => new SelectListItem()
                {
                    Text = x,
                    Value = x
                }).Distinct().ToList();
                Titles title = new Titles();
                title.TitleSearch = titleSearch;
                title.TitleSearch.ContentDistributors = searchValues.ContentDistributors;
                title.TitleSearch.ContentProviders = searchValues.ContentProviders;
                title.TitleSearch.SelectedTitleList = searchValues.SelectedTitleList;
                title.TitleSearch.EditTypes = searchValues.EditTypes;
                title.TitleSearch.TerritoryList = searchValues.TerritoryList;
                title.TitleSearch.LanguageList = searchValues.LanguageList;
                title.TitleSearch.VideoVersionList = searchValues.VideoVersionList;
                title.TitleSearch.MPMList = searchValues.MPMList;
                title.TitleSearch.VendorIdList = searchValues.VendorIdList;
                title.TitleSearch.ComponentTypes = searchValues.ComponentTypes;
                title.TitleSearch.SortOrderList = searchValues.SortOrderList;
                title.TitleSearch.SortByList = searchValues.SortByList;
                title.TitleSearch.ChannelDateRangeList = searchValues.ChannelDateRangeList;
                title.TitleSearch.RegionList = searchValues.RegionList;
                if (!ModelState.IsValid)
                {
                    title.TitleList = new List<TitleList>().ToPagedList(1, 10);
                    return View("Titles", title);
                }

                title.TitleList = _title.SearchTitles(titleSearch).ToPagedList(pageNumber ?? 1, 10);
                return View("Titles", title);
            }
            catch (Exception ex)
            {
                return View("Error");
            }


        }


        /// <summary>
        /// Export titles to .xlsx file 
        /// </summary>
        /// <param name="titleSearch">titleSearch contains filter values to export tiles to .xlsx file</param>
        /// <returns>FileResult which help to download file in browser</returns>
        [HttpGet]
        public ActionResult ExportTitle(TitleSearch titleSearch)
        {
            //Titles(TitleSearch titleSearch, int? pageNumber)
            IExportReportService _export = new ExportReportService();
            int recordCount = _export.GetTitleReportRecordCount(titleSearch);
            if (recordCount > App.Config.MaxTitleReportDownloadRecordLimit)
            {
                titleSearch.Territory = string.IsNullOrEmpty(titleSearch.TerritoryConcat) ? titleSearch.Territory : titleSearch.TerritoryConcat.Split('|');
                titleSearch.Language = string.IsNullOrEmpty(titleSearch.LanguageConcate) ? titleSearch.Language : titleSearch.LanguageConcate.Split('|');
                titleSearch.Region = string.IsNullOrEmpty(titleSearch.RegionConcat) ? titleSearch.Region : titleSearch.RegionConcat.Split('|');
                TempData["ErrorMsg"] = string.Format("There are {0} records that you are trying to download. Record count should not exceed {1} records.", recordCount, App.Config.MaxTitleReportDownloadRecordLimit);
                return RedirectToAction("Titles", "Title", new { @titleSearch = titleSearch });
                //return RedirectToAction("Index");
            }

            DownLoadFile downloadFile = _export.GenerateTitleReport(titleSearch);
            return File(downloadFile.bufferByte, "application/vnd.ms-excel", downloadFile.FileName);
        }
    }
}