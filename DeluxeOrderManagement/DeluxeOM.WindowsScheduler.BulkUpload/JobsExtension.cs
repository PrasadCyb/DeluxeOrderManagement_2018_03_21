using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.BulkUploader;
using DeluxeOM.Models.BulkUploader;
using DeluxeOM.Announcement.BulkUploader;
using DeluxeOM.VID.BulkUploader;
using DeluxeOM.PO.BulkUploader;
using DeluxeOM.PR.BulkUploader;
using DeluxeOM.QC.BulkUploader;
using DeluxeOM.Title.BulkUploader;
using System.Diagnostics;

namespace DeluxeOM.WindowsScheduler.BulkUpload
{
    public class JobsExtension
    {
        /// <summary>
        /// Bulid job args
        /// </summary>
        /// <param name="args">Consist parameter for executing selected job</param>
        /// <remark>This method decides which job to execute</remark>
        /// <returns>JobArgs</returns>
        public static JobArgs BuildJobArgs(string[] args)
        {
            //int maxArgCnt = args.Count();
            JobArgs jobArgs = new JobArgs()
            {
                TriggeredBy = args.Length >= 3 ? args[2].Replace('_', ' ') : "SYSTEM",
                FileName = args.Length >= 4 ? args[3].Replace('%', ' ') : null,  
            };
            switch (args[0].ToString().ToLower())
            {
                case "announcement":
                case "ann":
                    {
                        jobArgs.JobType = Models.JobType.Announceemnt;
                        if (args.Length >= 2)
                            jobArgs.FirstAnnouncedDate = DateTime.ParseExact(args[1], "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    }
                case "pipelineorder":
                case "po":
                    {
                        jobArgs.JobType = Models.JobType.PipelineOrder;
                        break;
                    }
                case "pricereport":
                case "pr":
                    {
                        jobArgs.JobType = Models.JobType.PriceReport;
                        break;
                    }
                case "qcreport":
                case "qc":
                    {
                        jobArgs.JobType = Models.JobType.QCReport;
                        break;
                    }
                case "titlereport":
                case "tr":
                    {
                        jobArgs.JobType = Models.JobType.TitleReport;
                        break;
                    }
            }
            return jobArgs;
        }

        /// <summary>
        /// Configure all task for slected job
        /// </summary>
        /// <param name="args">Consist current user name, job type and file name</param>
        /// <returns>Job which consist all tasks for the selected job</returns>
        public static IJob ConfigureJob(JobArgs args)
        {
            IJob job = null;

            switch (args.JobType)
            {
                case Models.JobType.Announceemnt:
                    {
                        string dt = args.FirstAnnouncedDate.HasValue ? args.FirstAnnouncedDate.Value.ToString("MM/dd/yyyy") : DateTime.Now.ToString("MM/dd/yyyy") ;
                        DateTime? firstAnnouncedDate = DateTime.ParseExact(dt, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        var tasks = new List<ITask>();

                        if (!args.IsBrowserUpload)
                        {
                            tasks.Add(new AnnouncementFTPDownloadTask());
                        }

                        tasks.Add(new AnnouncementBulkInsertTask(args.FileName));
                        tasks.Add(new ProcessAnnouncementTask(firstAnnouncedDate));

                        if (!args.IsBrowserUpload)
                        {
                            tasks.Add(new FTPAnnouncementArchiveTask());
                        }
                        job = new DeluxeOM.Announcement.BulkUploader.AnnouncementUploaderJob(tasks);

                        break;
                    }
                case Models.JobType.PipelineOrder:
                    {
                        var tasks = new List<ITask>();
                        if (!args.IsBrowserUpload)
                        {
                            tasks.Add(new PipeLineOrderFTPDownloadTask());
                        }

                        tasks.Add(new PipeLineOrderBulkInsertTask(args.FileName));
                        tasks.Add(new ProcessPipeLineOrderTask());

                        if (!args.IsBrowserUpload)
                        {
                            tasks.Add(new FTPPipelineOrderArchiveTask());
                        }
                        job = new DeluxeOM.PO.BulkUploader.PipeLineOrderUploaderJob(tasks);
                        break;
                    }
                case Models.JobType.PriceReport:
                    {
                        var tasks = new List<ITask>();
                        if (!args.IsBrowserUpload)
                        {
                            tasks.Add(new PriceReportFTPDownloadTask());
                        }

                        tasks.Add(new PriceReportBulkInsertTask(args.FileName));
                        tasks.Add(new ProcessPriceReportTask());

                        if (!args.IsBrowserUpload)
                        {
                            tasks.Add(new FTPPriceReportArchiveTask());
                        }
                        

                        job = new DeluxeOM.PR.BulkUploader.PriceReportUploaderJob(tasks);
                        break;
                    }
                case Models.JobType.QCReport:
                    {
                        var tasks = new List<ITask>();
                        if (!args.IsBrowserUpload)
                        {
                            tasks.Add(new QCReportFTPDownloadTask());
                        }

                        tasks.Add(new QCReportBulkInsertTask(args.FileName));
                        tasks.Add(new ProcessQCReportTask());

                        if (!args.IsBrowserUpload)
                        {
                            tasks.Add(new FTPQCReportArchiveTask());
                        }
                        
                        job = new DeluxeOM.QC.BulkUploader.QCReportUploaderJob(tasks);
                        break;
                    }
                case Models.JobType.TitleReport:
                    {
                        var tasks = new List<ITask>()
                        {
                            new ProcessTitleTask()
                        };
                        job = new DeluxeOM.Title.BulkUploader.ProcessTitleJob(tasks);
                        break;
                    }
            }
            job.RunBy = string.IsNullOrEmpty(args.TriggeredBy) ? "SYSTEM" : args.TriggeredBy.Replace('_', ' ') ;
            return job;
        }
    }
}
