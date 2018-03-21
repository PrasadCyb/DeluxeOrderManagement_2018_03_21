using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DeluxeOM.WCFServiceLib
{ 
    [ServiceContract]
    public interface IOrderMgtService
    {
        [OperationContract]
        void InvokeApp(string processType, DateTime firstAnnouncedDate, string userName,string fileName);
        
    }
}
