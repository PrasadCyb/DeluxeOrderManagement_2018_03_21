using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;

namespace DeluxeOM.Repository
{
    /// <summary>
    /// Contains methods for search Title
    /// </summary>
    public class TitleRepository : ITitleRepository
    {

        /// <summary>
        /// Populate all filter values on page load
        /// </summary>
        /// <returns>Filters value</returns>
        public TitleSearch GetSearchValues()
        {
            TitleSearch titleSearch = new TitleSearch();
            var _context = new DeluxeOrderManagementEntities();
            var selectedTitles = (from vid in _context.VIDs
                                  select vid.TitleName.ToString()).Distinct().OrderBy(x => x).ToList();
            var titleCategory = (from ann in _context.OrderGrids
                                 select ann.Category.ToString()).Distinct().OrderBy(x => x).ToList();
            var contentProviders = (from customer in _context.Customers
                                    where customer.Type == (int)Customers.ContentProvider
                                    select customer.Name.ToString()
                                  ).Distinct().ToList();
            var contentDistributors = (from customer in _context.Customers
                                       where customer.Type == (int)Customers.ContentDistributor
                                       select customer.Name.ToString()
                                  ).Distinct().ToList();
            var territories = (from territory in _context.Territories
                               where territory.AppleTerritory!=null
                               select territory.AppleTerritory.ToString()).Distinct().OrderBy(x => x).ToList();
            var languages = (from language in _context.Languages
                             select language.Name.ToString()).Distinct().OrderBy(x => x).ToList();
            var videoVersions = (from vid in _context.VIDs
                                select vid.VideoVersion.ToString()).Distinct().OrderBy(x => x).ToList();
            var vendorIds = (from vid in _context.VIDs
                             where !string.IsNullOrEmpty(vid.VendorId)
                             select vid.VendorId.ToString()).Distinct().OrderBy(x => x).ToList();
            var mpm = (from vid in _context.VIDs
                       select vid.MPM.ToString()).Distinct().OrderBy(x => x).ToList();
            var regions = (from reg in _context.Regions
                       select reg.NAME.ToString()).Distinct().OrderBy(x => x).ToList();
            titleSearch.SelectedTitles = selectedTitles;
            titleSearch.EditTypes = titleCategory;
            titleSearch.ContentDistributors = contentDistributors;
            titleSearch.ContentProviders = contentProviders;
            titleSearch.Territories = territories;
            titleSearch.Languages = languages;
            titleSearch.VideoVersions = videoVersions;
            titleSearch.MPMs = mpm;
            titleSearch.VendorIds = vendorIds;
            titleSearch.Regions = regions;
            return titleSearch;
        }


        /// <summary>
        /// Search title on basis of selected filter criteria
        /// </summary>
        /// <param name="whrSQLquery">Filters values concatenated into a string</param>
        /// <returns>List of Titles</returns>
        public List<TitleList> GetTitles(string whrSQLquery)
        {
            var _context = new DeluxeOrderManagementEntities();
            var titles = _context.usp_Search_Title(whrSQLquery).Select(x =>
                                                                new TitleList
                                                                {
                                                                    TitleName = x.TitleName,
                                                                    vendorID = x.VendorId,
                                                                    VIDType = x.VIDStatus,
                                                                    Territory = x.AppleTerritory,
                                                                    ApplePOESTStartDate = x.POESTStartDate,
                                                                    AppleESTStartDate = x.ESTStartDate,
                                                                    AppleESTEndDate=x.ESTEndDate,

                                                                    ESTHDLiveStatus = x.LiveESTHD,
                                                                    ESTSDLiveStatus = x.LiveESTSD,
                                                                    AppleVODStartDate = x.VODStartDate,
                                                                    AppleVODEndDate = x.VODEndDate,
                                                                    VODHDLiveStatus = x.LiveVODHD,
                                                                    VODSDLiveStatus = x.LiveVODSD,
                                                                    AnnouncementPOESTStartDate = x.AnnPOESTStartDate,
                                                                    AnnouncementESTStartDate = x.AnnESTStartDate,
                                                                    AnnouncementESTEndDate = x.AnnESTEndDate,
                                                                    AnnouncementVODStartDate = x.AnnVODStartDate,
                                                                    AnnouncementVODEndDate = x.AnnVODEndDate,
                                                                    ComponentType = x.ComponentType,
                                                                    ComponentQuality = x.ComponentQuality,
                                                                    Language = x.LanguageName,
                                                                    LanguageType = x.LanguageType,
                                                                    LanguageStatus=x.LanguageStatus
                                                                }).Distinct().ToList();
            return titles;
        }

        /// <summary>
        /// Search title on basis of selected filter criteria
        /// </summary>
        /// <param name="whrSQLquery">Filters values concatenated into a string</param>
        /// <returns>List of Titles</returns>
        public List<TitleList> SearchTitles(string whrSQLquery)
        {
            var _context = new DeluxeOrderManagementEntities();
            var titles = _context.usp_Search_Title(whrSQLquery).Select(x =>
                                                                new TitleList
                                                                {
                                                                    TitleName = x.TitleName,
                                                                    vendorID = x.VendorId,
                                                                    VIDType = x.VIDStatus,
                                                                    Territory = x.AppleTerritory,
                                                                    ApplePOESTStartDate = x.POESTStartDate,
                                                                    AppleESTStartDate = x.ESTStartDate,
                                                                    AppleESTEndDate = x.ESTEndDate,
                                                                    ESTHDLiveStatus = x.LiveESTHD,
                                                                    ESTSDLiveStatus = x.LiveESTSD,
                                                                    AppleVODStartDate = x.VODStartDate,
                                                                    AppleVODEndDate = x.VODEndDate,
                                                                    VODHDLiveStatus = x.LiveVODHD,
                                                                    VODSDLiveStatus = x.LiveVODSD,
                                                                    AnnouncementPOESTStartDate = x.AnnPOESTStartDate,
                                                                    AnnouncementESTStartDate = x.AnnESTStartDate,
                                                                    AnnouncementESTEndDate = x.AnnESTEndDate,
                                                                    AnnouncementVODStartDate = x.AnnVODStartDate,
                                                                    AnnouncementVODEndDate = x.AnnVODEndDate,
                                                                    ComponentType = x.ComponentType,
                                                                    ComponentQuality = x.ComponentQuality,
                                                                    Language = x.LanguageName,
                                                                    LanguageType = x.LanguageType,
                                                                    LanguageStatus = x.LanguageStatus
                                                                }).Distinct().ToList();
            return titles;
        }
    }
}
