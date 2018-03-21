using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;

namespace DeluxeOM.Repository
{
    public interface IVIDRepository
    {
        List<ImportValidationResult> Import(string filePath, int userId);

        SaveResult Save(VID vid);

        VID GetVidByVVID(string videoVersion, string vendorId);

        List<VID> GetVIDByVideoVersion(string videoVersion);
        VIDSearch GetSearchValue();
    }
}
