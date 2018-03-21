using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DeluxeOM.Utils.Config
{
    [ConfigurationCollection(typeof(FtpConfigElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class DeluxeAppSettingsConfigCollection : ConfigurationElementCollection
    {
        public DeluxeAppSettingsConfigElement this[int index]
        {
            get
            {
                return (DeluxeAppSettingsConfigElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public DeluxeAppSettingsConfigElement this[string key]
        {
            get
            {
                return (DeluxeAppSettingsConfigElement)BaseGet(key);
            }
        }

        public void Add(DeluxeAppSettingsConfigElement serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DeluxeAppSettingsConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DeluxeAppSettingsConfigElement)element).Key;
        }

        public void Remove(DeluxeAppSettingsConfigElement serviceConfig)
        {
            BaseRemove(serviceConfig.Key);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(String name)
        {
            BaseRemove(name);
        }

        public new IEnumerator<DeluxeAppSettingsConfigElement> GetEnumerator()
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
            {
                yield return base.BaseGet(i) as DeluxeAppSettingsConfigElement;
            }
        }
    }
}
