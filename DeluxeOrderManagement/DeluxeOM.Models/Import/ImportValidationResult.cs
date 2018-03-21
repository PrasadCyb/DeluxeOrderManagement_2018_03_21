using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models
{
    public class ImportValidationResult
    {
        public int Id { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public string ValidationLevel { get; set; }
        public string ValidationMessage { get; set; }
    }
}
