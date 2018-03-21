using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;
using DeluxeOM.Repository;

namespace DeluxeOM.Services
{
    public class TitleService : ITitleService
    {

        ITitleRepository _repository = null;
        public TitleService()
        {
            _repository = new TitleRepository();
        }

        /// <summary>
        /// Populate filter values of Title
        /// </summary>
        /// <returns>TitleSearch</returns>
        public TitleSearch GetSearchValues()
        {
            var searchValues = _repository.GetSearchValues();
            searchValues.ComponentTypes = new List<string>() { "Audio", "Video","Sub" };
            searchValues.SortByList = new List<KeyValue>()
            {
                new KeyValue {Text="Title Name", Value="TitleName" },
                new KeyValue {Text="Vendor ID", Value="VendorId" },
                new KeyValue {Text="VID Status", Value="VIDStatus" },
                new KeyValue {Text="Territory", Value="AppleTerritory" },
                new KeyValue {Text="Language", Value="LanguageName" },
                new KeyValue {Text="Pre Order Start Date", Value="POESTStartDate" },
                new KeyValue {Text="EST Start Date", Value="ESTStartDate" },
                new KeyValue {Text="EST End Date", Value="ESTEndDate" },
                new KeyValue {Text="ESTHD Live Status", Value="LiveESTHD" },
                new KeyValue {Text="ESTSD Live Status", Value="LiveESTSD" },
                new KeyValue {Text="VOD Start Date", Value="VODStartDate" },
                new KeyValue {Text="VOD End Date", Value="VODEndDate" },
                new KeyValue {Text="VODHD Live Status", Value="LiveVODHD" },
                new KeyValue {Text="VODSD Live Status", Value="LiveVODSD" },
                new KeyValue {Text="Component Type", Value="ComponentType" },
                new KeyValue {Text="Component Quality", Value="ComponentQuality" },
                new KeyValue {Text="Language Type", Value="LanguageType" },
                new KeyValue {Text="Language Status", Value="LanguageStatus" },
            };
            searchValues.SortOrderList = new List<KeyValue>()
            {
                new KeyValue {Text="Ascending", Value="asc" },
                new KeyValue {Text="Descending", Value="desc" }
            };
            searchValues.ChannelDateRangeList = new List<KeyValue>()
            {
                new KeyValue {Text="EST Start Date", Value="est" },
                new KeyValue {Text="POEST Start Date", Value="poest" }
            };
            return searchValues;
        }

        /// <summary>
        /// Populates all Titles
        /// </summary>
        /// <returns>TitleList</returns>
        public List<TitleList> GetTitles()
        {
            string whrSQLquery = string.Empty;
            var titles=_repository.GetTitles(whrSQLquery);
            return titles;
        }

        /// <summary>
        /// Search title on the basis of selected filter criteria
        /// </summary>
        /// <param name="titleSearch">titleSearch contains filter values to search titles from a database</param>
        /// <returns>TitleList</returns>
        public List<TitleList> SearchTitles(TitleSearch titleSearch)
        {
            string whrSQLquery = string.Empty;
            string temp = string.Empty;
            if (!string.IsNullOrEmpty(titleSearch.StartDate) && !string.IsNullOrEmpty(titleSearch.EndDate))
            {
                var channelStart = DateTime.ParseExact(titleSearch.StartDate, "MM/dd/yyyy", null);
                var channelEnd = DateTime.ParseExact(titleSearch.EndDate, "MM/dd/yyyy", null);
                if (titleSearch.ChannelDateRange.ToLower() == "est")
                {
                    titleSearch.ESTStartDate = channelStart;
                    titleSearch.ESTEndDate = channelEnd;
                }
                else if (titleSearch.ChannelDateRange.ToLower() == "poest")
                {
                    titleSearch.POESTStart = channelStart;
                    titleSearch.POESTEnd = channelEnd;
                }
            }
            if (!string.IsNullOrEmpty(titleSearch.SelectedTitle))
            {
                temp = titleSearch.SelectedTitle;
                titleSearch.SelectedTitle = titleSearch.SelectedTitle.Replace("'", "\''");
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " TitleName ='" + titleSearch.SelectedTitle + "'  " : whrSQLquery + " AND TitleName = '" + titleSearch.SelectedTitle + "' ";
            }
            if (!string.IsNullOrEmpty(titleSearch.EditType))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " Category ='" + titleSearch.EditType + "'  " : whrSQLquery + " AND Category = '" + titleSearch.EditType + "' ";
            }
            if (titleSearch.Region != null && titleSearch.Region.Any())
            {
                var concat = titleSearch.Region.Aggregate((current, next) => current + "', '" + next);
                concat = string.Format("{0}{1}{0}", "'", concat);
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " RegionName IN(" + concat + ") " : whrSQLquery + " AND RegionName IN(" + concat + ") ";
            }
            if (titleSearch.Territory != null && titleSearch.Territory.Any())
            {
                var concatterritory = titleSearch.Territory.Aggregate((current, next) => current + "', '" + next);
                concatterritory = string.Format("{0}{1}{0}", "'", concatterritory);
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " AppleTerritory IN(" + concatterritory + ") " : whrSQLquery + " AND AppleTerritory IN(" + concatterritory + ") ";
            }

            if (titleSearch.Language!=null && titleSearch.Language.Any())
            {
                var concateLanguage = titleSearch.Language.Aggregate((current, next) => current + "', '" + next);
                concateLanguage = string.Format("{0}{1}{0}", "'", concateLanguage);
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " LanguageName IN("+ concateLanguage + ")  " : whrSQLquery + " AND LanguageName IN(" + concateLanguage + ") ";
            }

            if (!string.IsNullOrEmpty(titleSearch.VideoVersion))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " VideoVersion ='" + titleSearch.VideoVersion + "'  " : whrSQLquery + " AND VideoVersion = '" + titleSearch.VideoVersion + "' ";
            }
            //EST/POEST : Filters on start date
            if ((titleSearch.ESTStartDate != null && !(titleSearch.ESTStartDate.Equals(DateTime.MinValue))) && (titleSearch.ESTEndDate != null && !(titleSearch.ESTEndDate.Equals(DateTime.MinValue))))
            {
                string estStartDate = titleSearch.ESTStartDate.Date.ToString("yyyy-MM-dd");
                string estEndDate = titleSearch.ESTEndDate.AddDays(1).Date.ToString("yyyy-MM-dd");
                //whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (ESTStartDate >='" + estStartDate + "' AND ESTEndDate <'" + estEndDate + "')  " : whrSQLquery + " AND (ESTStartDate >='" + estStartDate + "' AND ESTEndDate <'" + estEndDate + "') ";
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (ESTStartDate >='" + estStartDate + "' AND ESTStartDate <'" + estEndDate + "')  " : whrSQLquery + " AND (ESTStartDate >='" + estStartDate + "' AND ESTStartDate <'" + estEndDate + "') ";
            }
            if ((titleSearch.POESTStart != null && !(titleSearch.POESTStart.Equals(DateTime.MinValue))) && (titleSearch.POESTEnd != null && !(titleSearch.POESTEnd.Equals(DateTime.MinValue))))
            {
                string poeststart = titleSearch.POESTStart.Date.ToString("yyyy-MM-dd");
                string poestend = titleSearch.POESTEnd.AddDays(1).Date.ToString("yyyy-MM-dd");
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " (POESTStartDate >='" + poeststart + "' AND POESTStartDate <'" + poestend + "')  " : whrSQLquery + " AND (POESTStartDate >='" + poeststart + "' AND POESTStartDate <'" + poestend + "') ";
            }

            if (!string.IsNullOrEmpty(titleSearch.MPM))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " MPM ='" + titleSearch.MPM + "'  " : whrSQLquery + " AND MPM = '" + titleSearch.MPM + "' ";
            }

            if (!string.IsNullOrEmpty(titleSearch.VendorId))
            {
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " VendorId ='" + titleSearch.VendorId + "'  " : whrSQLquery + " AND VendorId = '" + titleSearch.VendorId + "' ";
            }

            if (!string.IsNullOrEmpty(titleSearch.ComponentType))
            {
                string componentTypes = string.Empty;
                if (titleSearch.ComponentType.Equals("Audio"))
                {
                    componentTypes = "'AUDIO'" + "," + "'FORCED_SUBTITLES'" + "," + "'DUB_CREDIT'" + "," + "'AUDIO_DESCRIPTION'";
                }
                else if (titleSearch.ComponentType.Equals("Sub"))
                {
                    componentTypes = "'SUBTITLES'" + "," + "'CAPTIONS'";
                }
                else if (titleSearch.ComponentType.Equals("Video"))
                {
                    componentTypes = "'VIDEO'";
                }
                whrSQLquery = string.IsNullOrEmpty(whrSQLquery) ? " ComponentType IN(" + componentTypes + ")" : whrSQLquery + " AND ComponentType IN(" + componentTypes + ") ";
            }

            
            if (!string.IsNullOrEmpty(whrSQLquery))
            {
                whrSQLquery = "where" + whrSQLquery;
            }

            if (!string.IsNullOrEmpty(titleSearch.SortBy))
            {
                whrSQLquery = whrSQLquery + " ORDER BY "+" "+ titleSearch.SortBy + " " + titleSearch.SortOrder;
            }
            // this if block is to resore title name which is modified for search title with '
            if (!string.IsNullOrEmpty(temp))
            {
                titleSearch.SelectedTitle = temp;
            }
            var titles = _repository.SearchTitles(whrSQLquery);
            return titles;
        }
    }
}
