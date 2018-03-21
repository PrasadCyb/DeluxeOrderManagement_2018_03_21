using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DeluxeOM.Utils.Config
{
    [ConfigurationCollection(typeof(FtpConfigElement), AddItemName = "ftp", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class FtpSettingsConfigCollection: ConfigurationElementCollection
    {
        public FtpConfigElement this[int index]
        {
            get
            {
                return (FtpConfigElement)BaseGet(index);
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

        new public FtpConfigElement this[string provider]
        {
            get
            {
                return (FtpConfigElement)BaseGet(provider);
            }
        }

        public void Add(FtpConfigElement serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FtpConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FtpConfigElement)element).Provider;
        }

        public void Remove(FtpConfigElement serviceConfig)
        {
            BaseRemove(serviceConfig.Provider);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(String name)
        {
            BaseRemove(name);
        }

        public new IEnumerator<FtpConfigElement> GetEnumerator()
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
            {
                yield return base.BaseGet(i) as FtpConfigElement;
            }
        }

    }
}
