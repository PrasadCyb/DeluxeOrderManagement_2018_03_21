using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;
using System.Data.SqlClient;

namespace DeluxeOM.Repository
{
    /// <summary>
    /// VID Repository class includes methods for Export, Import and Addition of new VID through the UI
    /// </summary>
    public class VIDRepository : IVIDRepository
    {
        /// <summary>
        /// Import VID list into system
        /// </summary>
        /// <param name="filePath">downloaded file path</param>
        /// <param name="userId">current userId</param>
        /// <returns>Validation result from BulkUpdate_Validations table</returns>
        public List<ImportValidationResult> Import(string filePath, int userId)
        {
            var _context = new DeluxeOrderManagementEntities();

            var fileParam = new SqlParameter
            {
                ParameterName = "filePath",
                Value = filePath
            };
            var userParam = new SqlParameter
            {
                ParameterName = "userId",
                Value = userId
            };
            return _context.Database.SqlQuery<BulkUpdate_Validations>("exec usp_IMPORT_VID @filePath, @userId", fileParam, userParam)
                .Select(x =>
                            new ImportValidationResult
                            {
                                Id = x.KeyId,
                                ColumnName = x.ColumnName,
                                ColumnValue = x.ColumnValue,
                                ValidationLevel = x.ValidationLevel,
                                ValidationMessage = x.ValidationMessage
                            }).ToList<ImportValidationResult>();
        }



        /// <summary>
        /// Add Title in VID 
        /// </summary>
        /// <param name="vid">This consists of all the required information need to form VID object which is further inserted into the database</param>
        /// <returns>Save Result</returns>
        public SaveResult Save(VID vid)
        {
            var _context = new DeluxeOrderManagementEntities();

            _context.VIDs.Add(vid);
            _context.SaveChanges();
            return new SaveResult();
        }

        /// <summary>
        /// Search vid based on videoVersion and vedorID
        /// </summary>
        /// <param name="videoVersion"></param>
        /// <param name="vendorId"></param>
        /// <returns>VID</returns>
        public VID GetVidByVVID(string videoVersion, string vendorId)
        {
            var _context = new DeluxeOrderManagementEntities();
            var vid = (from v in _context.VIDs
                       where v.VideoVersion == videoVersion && v.VendorId == vendorId
                       select v
                      ).FirstOrDefault();
            return vid;
        }

        /// <summary>
        /// Search VID by Video version
        /// </summary>
        /// <param name="videoVersion"></param>
        /// <returns>List of VID</returns>
        public List<VID> GetVIDByVideoVersion(string videoVersion)
        {
            var _context = new DeluxeOrderManagementEntities();
            var vid = (from v in _context.VIDs
                       where v.VideoVersion == videoVersion
                       select v
                      ).Distinct().ToList();
            return vid;
        }


        /// <summary>
        /// Populate all dropdown value on VID page and render view
        /// </summary>
        /// <returns>VID Search</returns>
        public VIDSearch GetSearchValue()
        {
            var _context = new DeluxeOrderManagementEntities();
            var titleName = (from vid in _context.VIDs
                             where !string.IsNullOrEmpty(vid.TitleName)
                             select vid.TitleName
                           ).Distinct().OrderBy(x=>x).ToList();
            var vendorId = (from vid in _context.VIDs
                             where !string.IsNullOrEmpty(vid.VendorId)
                             select vid.VendorId
                           ).Distinct().OrderBy(x => x).ToList();
            var videoVersion = (from vid in _context.VIDs
                            where !string.IsNullOrEmpty(vid.VideoVersion)
                            select vid.VideoVersion
                           ).Distinct().OrderBy(x => x).ToList();
            var vidSatus = (from vid in _context.VIDs
                                where !string.IsNullOrEmpty(vid.VIDStatus)
                                select vid.VIDStatus
                           ).Distinct().OrderBy(x => x).ToList();
            VIDSearch vidSeach = new VIDSearch();
            vidSeach.TitleList = titleName.Select(x=> new KeyValue {Text=x,Value=x }).ToList();
            vidSeach.VendorIdList = vendorId.Select(x => new KeyValue { Text = x, Value = x }).ToList();
            vidSeach.VideoVersionList = videoVersion.Select(x => new KeyValue { Text = x, Value = x }).ToList();
            vidSeach.ListVidStatus = vidSatus.Select(x => new KeyValue { Text = x, Value = x }).ToList();
            return vidSeach;
        }
    }
}
