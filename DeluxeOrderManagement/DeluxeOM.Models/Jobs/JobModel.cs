using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class JobModel
    {
        public JobModel()
        {
            this.JobSearch = new JobSearch();
        }

        public JobSearch JobSearch { get; set; }
    }

}