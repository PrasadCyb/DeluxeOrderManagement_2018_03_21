using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;

namespace DeluxeOM.Services
{
    public class AssetTrackerService : IAssetTrackerService
    {
        public AssetTracker GetAsset()
        {
            AssetTracker assetTracker = new AssetTracker();
            assetTracker.AssetDetails = new List<AssetDeails>();
            assetTracker.TerritoryVID = new List<TerritoryVID>();
            assetTracker.TrailerVID = new List<TrailerVID>();
            AssetDeails assetDetails = new AssetDeails()
            {
                LanguageName="English",
                LanguageCode="En",
                Quality="5.1",
                AudStatus="Approved",
                AudImportDate= "18-Jan-2018",
                SubStatus="In-Review",
                SubImportDate= "18-Jan-2018",
                YesNo="Y",
                AudDesQuality="5.1",
                AudDesStatus="Appoved",
                AudDesImportDate ="18-Jan-2018",
                
            };
            TerritoryVID territoryVID = new TerritoryVID()
            {
                ESTLive="Yes",
                POESTLive="Yes",
                TerritoryName="Anguilla",
                VODLive="Yes"
            };
            TrailerVID trailerVID = new TrailerVID()
            {
                ImportDate = "18-Jan-2018",
                Quality = "5.1",
                Status = "Approved",
                TrailerTerritory="Anguilla"
            };
            for (int i = 0; i < 40; i++)
            {
                assetTracker.AssetDetails.Add(assetDetails);
            }
            assetTracker.AssetDetails.Add(assetDetails);
            for (int i = 0; i < 20; i++)
            {
                assetTracker.TerritoryVID.Add(territoryVID);
            }
            assetTracker.TerritoryVID.Add(territoryVID);
            assetTracker.TrailerVID.Add(trailerVID);
            assetTracker.TrailerVID.Add(trailerVID);
            return assetTracker;
        }
    }
}
