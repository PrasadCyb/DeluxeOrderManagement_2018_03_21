using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class BulkOrderUpdateResult
    {
        public int OrderId { get; set; }
        public string ColumnName { get; set; }
        public bool UploadStatus { get; set; }
        public string Message { get; set; }
    }
}
