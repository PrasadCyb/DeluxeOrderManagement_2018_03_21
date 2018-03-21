using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DeluxeOM.WCFServiceLib;
using System.Diagnostics;
using System.Configuration;

namespace DeluxeOM.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrderMgtService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrderMgtService.svc or OrderMgtService.svc.cs at the Solution Explorer and start debugging.
    public class OrderMgtService : IOrderMgtService
    {
        
        /// <summary>
        /// Call main method of Console(.exe UI) application to execute jobs
        /// </summary>
        /// <param name="processType">Job type</param>
        /// <param name="firstAnnouncedDate">First announcement date</param>
        /// <param name="userName">Current user name</param>
        /// <param name="fileName">Browse file name</param>
        public void InvokeApp(string processType, DateTime firstAnnouncedDate, string userName, string fileName)
        {
            /*to debug the code only*/
            if (!EventLog.SourceExists("DeluxeOrderMgt"))
                EventLog.CreateEventSource("DeluxeOrderMgt", "Application");

            EventLog.WriteEntry("DeluxeOrderMgt", "In DeluxeOrderMgt WCF Service");
            

            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = string.Format("{0} {1} {2} {3}", processType, firstAnnouncedDate.ToString("MM/dd/yyyy"), userName.Replace(' ', '_'), fileName );
            // Enter the executable to run, including the complete path
            start.FileName = ConfigurationManager.AppSettings["exePath"];

            EventLog.WriteEntry("DeluxeOrderMgt", "WCFService.InvokeApp() : Calling " + start.FileName);

            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;

            Process.Start(start);

            //System.Threading.Thread.Sleep(2000);
            EventLog.WriteEntry("DeluxeOrderMgt", "WCFService.InvokeApp() : Process started");
        }
    }
}
