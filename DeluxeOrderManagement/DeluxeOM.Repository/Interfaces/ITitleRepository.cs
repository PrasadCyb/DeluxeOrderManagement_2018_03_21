using DeluxeOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Repository
{
    public interface ITitleRepository
    {
        TitleSearch GetSearchValues();
        List<TitleList> GetTitles(string whrSQLquery);
        List<TitleList> SearchTitles(string whrSQLquery);
    }
}
