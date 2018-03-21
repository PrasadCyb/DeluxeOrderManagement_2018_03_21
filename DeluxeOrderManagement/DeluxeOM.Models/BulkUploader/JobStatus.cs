using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Models.BulkUploader
{
    public class JobStatus
    {
        public int JobId { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public string FileName { get; set; }

        public int NoOfRecordsImported { get; set; }

        public static JobStatus DlxSuccessResult()
        {
            return new JobStatus { Success = true };
        }
    }

    public class dlxTaskResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public dlxTaskData TaskData { get; set; }

        public static dlxTaskResult SuccessResult()
        {
            return new dlxTaskResult() { IsSuccess = true };
        }
    }

    public class dlxTaskData
    {
        public string FilePath { get; set; }

        public int NoOfRecordImported { get; set; }
    }

    public class JobArgs
    {
        public JobType JobType { get; set; }
        public string FileName { get; set; }
        public DateTime? FirstAnnouncedDate { get; set; }
        public string TriggeredBy { get; set; }

        public bool IsBrowserUpload
        {
            get
            {
                return !string.IsNullOrEmpty(this.FileName);
            }
        }
        
    }
}
