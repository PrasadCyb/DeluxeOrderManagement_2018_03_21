using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;
using DeluxeOM.Repository;

namespace DeluxeOM.Services
{
    public interface IVidService
    {
        List<ImportValidationResult> Import(string filePath, int userId);
        SaveResult AddVid(VIDModel vidModel);
        VIDModel GetDropDownValue();
        VIDSearch GetSearchValue();
        
    }
}
