using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.BulkUploader
{
    public interface ITask
    {
        string Name { get; }

        dlxTaskResult PreviousTaskResult { get; set; }
        void Execute(int id  );
        dlxTaskResult GetTaskResult();
    }
}
