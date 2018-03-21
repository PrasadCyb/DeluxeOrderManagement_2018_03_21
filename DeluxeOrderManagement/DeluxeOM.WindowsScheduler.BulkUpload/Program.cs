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
    class Program
    {
        private static  string logSource = "DeluxeOrderMgt";

        /// <summary>
        /// Execute selected job 
        /// </summary>
        /// <param name="args">Consist parameter for executing selected job</param>
        static void Main(string[] args)
        {
            //Need to create an Object to hold args
            if (!args.Any())
            {
                return;
            }
            if (!EventLog.SourceExists(logSource))
                EventLog.CreateEventSource(logSource, "Application");

            JobArgs jobArgs = JobsExtension.BuildJobArgs(args);
            //int maxArgCouint = 3;
            //IJob job = null;
            IJob job = JobsExtension.ConfigureJob(jobArgs);

            //EventLog.WriteEntry(logSource, "WBAGScheduler Started. Args[0]: " + args[0] );

           //

            var jobStatus = job.Process();
            job.ProcessNotifications(jobStatus);
        }

        




    }
}
