using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class TerritoryLanguage
    {
        public List<Title> Title { get; set; }
        public List<Territory> Territory { get; set; }
        public List<Language> Language { get; set; }
        public List<ChkValues> Assets { get; set; }
        public List<string> LocalEdits { get; set; }
        public List<string> orderCategory { get; set; }
        public List<string> OrderTypes { get; set; }
    }
}
