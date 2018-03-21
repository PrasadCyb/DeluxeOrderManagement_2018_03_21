using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration ;

namespace DeluxeOM.Utils.Config
{
    
    public class SmtpMailConfigElement : System.Configuration.ConfigurationElement
    {
        [ConfigurationProperty("server")]
        public string Server
        {
            get
            {
                return base["server"] as string;
            }
        }

        [ConfigurationProperty("port")]
        public string Port
        {
            get
            {
                return base["port"] as string;
            }
        }

        [ConfigurationProperty("userName")]
        public string UserName
        {
            get
            {
                return base["userName"] as string;
            }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get
            {
                return base["password"] as string;
            }
        }

        [ConfigurationProperty("enableSSL")]
        public bool EnableSSL
        {
            get
            {
                return base["enableSSL"].ToString().ToLower() == "true" ? true : false;
            }
        }




    }
}
