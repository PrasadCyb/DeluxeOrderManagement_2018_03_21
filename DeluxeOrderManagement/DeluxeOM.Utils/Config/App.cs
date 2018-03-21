using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeOM.Utils.Config
{
    public class App
    {
        public static DeluxeConfigurationSection Config
        {
            get
            {
                return System.Configuration.ConfigurationManager.GetSection("deluxeConfiguration") as DeluxeConfigurationSection; 
            }
        }
        //App _app = null;

        //App GetInstance()
        //{
        //    //Config = Config ?? new DeluxeConfigurationSection();
        //    Config = System.Configuration.ConfigurationManager.GetSection("deluxeConfigurationSection") as DeluxeConfigurationSection;
        //    return _app ?? new App();
        //}
        //private App()
        //{
        //    //Config = new DeluxeConfigurationSection();
        //    //Configuration.Initialize();
        //}
    }
}
