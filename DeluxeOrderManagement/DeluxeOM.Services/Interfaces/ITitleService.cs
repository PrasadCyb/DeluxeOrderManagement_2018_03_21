using DeluxeOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Services
{
    public interface ITitleService
    {
        TitleSearch GetSearchValues();
        List<TitleList> GetTitles();
        List<TitleList> SearchTitles(TitleSearch titleSearch);
    }
}
