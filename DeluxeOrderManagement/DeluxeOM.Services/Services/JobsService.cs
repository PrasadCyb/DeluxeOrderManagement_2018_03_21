using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.BulkUploader;
using System.ServiceModel;
using DeluxeOM.WCFServiceLib;
using DeluxeOM.Utils.Config;
using DeluxeOM.Models;
using DeluxeOM.Repository;
using System.Diagnostics;
using System.IO;

namespace DeluxeOM.Services
{
    public class JobsService : ServiceBase, IJobsService
    {
        private IJobsRepository _repository = null;
        private IBulkUploadRepository _bulkUplaodRepositoy = null;
        private string logSource = "DeluxeOrderMgt";

        public JobsService()
        {
            if (!EventLog.SourceExists(logSource))
                EventLog.CreateEventSource(logSource, "Application");

            _repository = new JobsRepository();
            _bulkUplaodRepositoy = new BulkUploadRepository();

        }

        /// <summary>
        /// Populates all jobs
        /// </summary>
        /// <returns>List of jobs</returns>
        public List<Job> GetAll()
        {
            var eties = _repository.GetAll();
            List<Job> jobs = new List<Job>();
            if (eties != null && eties.Any())
            {
                foreach (var ety in eties)
                    jobs.Add(this.Mapper.CreateJobModel(ety));
            }
            return jobs;
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Job</returns>
        public Job GetById(int id)
        {
            //ForNow : Not necessarily called from repository
            return new Job();
        }

        /// <summary>
        /// Search details of selected job
        /// </summary>
        /// <param name="jobId">Id of selected job</param>
        /// <returns>Job details</returns>
        public List<JobItem> GetJobsItems(int jobId)
        {
            var eties = _repository.GetJobsItems(jobId);
            List<JobItem> jobs = new List<JobItem>();
            if (eties != null && eties.Any())
            {
                foreach (var ety in eties)
                    jobs.Add(this.Mapper.CreateJobItemModel(ety));
            }
            return jobs;
        }


        /// <summary>
        /// Call main method of Console(.exe UI) application to execute jobs
        /// </summary>
        /// <param name="fileType">Job type</param>
        /// <param name="announcementDate">First announcement date</param>
        /// <param name="loggedinUser">Current user name</param>
        /// <param name="fileName">Browse file name</param>
        public void Run(string fileType, DateTime announcementDate, string loggedinUser, string fileName)
        {
            //Create a ChannelFactory  
            //Required Namespace: using System.ServiceModel;  
            //Required Namespace: using ServiceLibrary;  
            ChannelFactory<IOrderMgtService> channelFactory = null;
            EventLog.WriteEntry(logSource, "JobsService.Run() started");

            try
            {
                //Create a binding of the type exposed by service  
                BasicHttpBinding binding = new BasicHttpBinding();

                //Create EndPoint address  
                EndpointAddress endpointAddress = new EndpointAddress(App.Config.WCFOrderMgtEndpoint);
                EventLog.WriteEntry(logSource, "JobsService.Run() : Endpoint Address : " + App.Config.WCFOrderMgtEndpoint); 

                //Pass Binding and EndPoint address to ChannelFactory  
                channelFactory = new ChannelFactory<IOrderMgtService>(binding, endpointAddress);
                EventLog.WriteEntry(logSource, "JobsService.Run() : Channel factory instantiated");

                //Now create the new channel as below  
                IOrderMgtService channel = channelFactory.CreateChannel();
                EventLog.WriteEntry(logSource, "JobsService.Run() : Channel factory created");

                EventLog.WriteEntry(logSource, "JobsService.Run() : calling WCF service");
                //Call the service method on this channel as below  
                channel.InvokeApp(fileType, announcementDate, loggedinUser, fileName);

                EventLog.WriteEntry(logSource, "JobsService.Run() : WCF service Invoked");
            }
            catch (TimeoutException)
            {
                //Timeout error  
                if (channelFactory != null)
                    channelFactory.Abort();

                EventLog.WriteEntry(logSource, "JobsService.Run() : Error Operation Timeout");

                throw;
            }
            catch (FaultException)
            {
                if (channelFactory != null)
                    channelFactory.Abort();

                EventLog.WriteEntry(logSource, "JobsService.Run() : Error Fault Exception");
                throw;
            }
            catch (CommunicationException)
            {
                //Communication error  
                if (channelFactory != null)
                    channelFactory.Abort();

                EventLog.WriteEntry(logSource, "JobsService.Run() : Error Communication Exception");
                throw;
            }
            catch (Exception)
            {
                if (channelFactory != null)
                    channelFactory.Abort();

                EventLog.WriteEntry(logSource, "JobsService.Run() : Error Exception");
                throw;
            }
        }

        /// <summary>
        /// Populates file upload path
        /// </summary>
        /// <param name="jobType">Contains which job to be executed</param>
        /// <param name="fileName">Name of file for which path is needed</param>
        /// <returns>File upload path</returns>
        public string GetFileUploadPath(string jobType,string fileName)
        {
            FtpConfiguration configType = new FtpConfiguration();
            switch (jobType.ToLower())
            {
                case "announcement":
                case "ann":
                    {
                        configType=_bulkUplaodRepositoy.GetFtpConfig(FtpConfigurationType.Announcement);
                        break;
                    }
                case "pipelineorder":
                case "po":
                    {
                        configType=_bulkUplaodRepositoy.GetFtpConfig(FtpConfigurationType.PipelineOrder);
                        break;
                    }
                case "pricereport":
                case "pr":
                    {
                        configType=_bulkUplaodRepositoy.GetFtpConfig(FtpConfigurationType.PriceReport);
                        break;
                    }
                case "qcreport":
                case "qc":
                    {
                        configType= _bulkUplaodRepositoy.GetFtpConfig(FtpConfigurationType.QCReport);
                        break;
                    }
            }
            if (!Directory.Exists(configType.DownloadLocalDirectory + configType.FtpDirecrory))
            {
                Directory.CreateDirectory(configType.DownloadLocalDirectory + configType.FtpDirecrory);
            }
            string filePath = string.Format("{0}{1}/{2}", configType.DownloadLocalDirectory, configType.FtpDirecrory, fileName);
            return filePath;
        }
    }
}
