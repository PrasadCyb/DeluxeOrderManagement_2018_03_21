using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.BulkUploader
{
    /// <summary>
    /// Used to find latest modified file from FTP
    /// </summary>
    public class FileModel
    {
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
