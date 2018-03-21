using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DeluxeOM.Utils.Config
{
    public class DeluxeAppSettingsConfigElement : System.Configuration.ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey =true)]
        public string Key
        {
            get
            {
                return base["key"] as string;
            }
        }

        [ConfigurationProperty("value")]
        public string Value
        {
            get
            {
                return base["value"] as string;
            }
        }
    }

    public class DeluxeAppSettings : List<DeluxeAppSettingsConfigElement>
    {
        public DeluxeAppSettingsConfigElement this[string key]
        {
            get
            {
                return this.SingleOrDefault(n => n.Key.ToString().ToLower() == key.ToLower());
            }
        }

        

    }

}
