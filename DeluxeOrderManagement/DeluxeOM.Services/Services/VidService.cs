using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;
using DeluxeOM.Repository;
using DeluxeOM.Services.Common;

namespace DeluxeOM.Services
{
    public class VidService: IVidService
    {
        IVIDRepository _repository = null;

        /// <summary>
        /// Create instance of VIDRepository
        /// </summary>
        public VidService()
        {
            _repository = new VIDRepository();
        }

        /// <summary>
        /// Add new Vid
        /// </summary>
        /// <param name="vidModel">This consists of all the required information need to form VID object which is further inserted into the database</param>
        /// <returns>SaveResult</returns>
        public SaveResult AddVid(VIDModel vidModel)
        {
            VID isVidExist = _repository.GetVidByVVID(vidModel.VideoVersion,vidModel.VendorId);
            var listVid = _repository.GetVIDByVideoVersion(vidModel.VideoVersion);
            if (isVidExist != null)
            {
                string existLocalEdit = string.IsNullOrEmpty(isVidExist.EditName) || isVidExist.EditName == "No" ? "No" : "Yes";
                string PresentLocalEdit = string.IsNullOrEmpty(vidModel.EditName) || vidModel.EditName == "No" ? "No" : "Yes";
                if (existLocalEdit.Equals(PresentLocalEdit))
                {
                    return SaveResult.FailureResult("Combination of Video Version, VendorId, LocalEdit is already present");
                }
            }
            if (listVid != null && listVid.Count > 0)
            {
                var titles = listVid.Select(x => x.TitleName).ToList();
                if (!titles.Contains(vidModel.TitleName))
                {
                    return SaveResult.FailureResult("Entered video version is already present with titlename" + " " + "'" + titles.FirstOrDefault() + "'");
                }
            }
            VID vid = Mapper.CreateVidEntity(vidModel);
            _repository.Save(vid);
            return new SaveResult() {Success=true, Message="VID Added successfully" };
        }

        /// <summary>
        /// Populates DropDown value
        /// </summary>
        /// <returns>VIDModel</returns>
        public VIDModel GetDropDownValue()
        {
            VIDModel vidModel = new VIDModel();
            vidModel.VidStatusList = new List<KeyValue>()
            {
                new KeyValue { Text="PRIMARY",Value="PRIMARY"},
                new KeyValue { Text="ACTIVE", Value="ACTIVE"}
            };
            return vidModel;
        }

        /// <summary>
        /// Populates filtered VID values
        /// </summary>
        /// <returns>VIDSearch</returns>
        public VIDSearch GetSearchValue()
        {
            var vidSearch = _repository.GetSearchValue();
            return vidSearch;
        }

        /// <summary>
        /// Import VID list into system
        /// </summary>
        /// <param name="filePath">This parameter consists of filePath of a selected .xlsx file through the UI</param>
        /// <param name="userId">Id of current user</param>
        /// <returns>List of ImportValidationResult</returns>
        public List<ImportValidationResult> Import(string filePath, int userId)
        {
            var result = _repository.Import(filePath, userId);
            
            return result;
        }

    }
}
