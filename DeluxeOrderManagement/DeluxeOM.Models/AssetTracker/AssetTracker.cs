using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class AssetTracker
    {
        public List<AssetDeails> AssetDetails { get; set; }
        public List<TerritoryVID> TerritoryVID { get; set; }
        public List<TrailerVID> TrailerVID { get; set; }
    }

    public class AssetDeails
    {
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }
        public string Quality { get; set; }
        public string AudStatus { get; set; }
        public string AudImportDate { get; set; }
        public string SubStatus { get; set; }
        public string SubImportDate { get; set; }
        public string YesNo { get; set; }
        public string AudDesQuality { get; set; }
        public string AudDesStatus { get; set; }
        public string AudDesImportDate { get; set; }

    }

    public class TerritoryVID
    {
        public string TerritoryName { get; set; }
        public string POESTLive { get; set; }
        public string ESTLive { get; set; }
        public string VODLive { get; set; }
    }

    public class TrailerVID
    {
        public string TrailerTerritory { get; set; }
        public string Quality { get; set; }
        public string Status { get; set; }
        public string ImportDate { get; set; }
    }

}
