using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Utils
{
    public static  class JobsConstants
    {
        public static Dictionary<string, string>  JobsList
        {
            get
            {
                return new Dictionary<string, string>
                {
                   {"po", "1. Pipeline Order"},
                   {"pr", "2. Price Report"},
                   {"qc", "3. QC Report"},
                   {"ann", "4. Announcement"},
                   {"tr", "Process Title Report"}
                };
            }
        }

        public static string SelectedJobKey
        {
            get
            {
                return "po";
            }
        }

    }
}
