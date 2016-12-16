using System.Configuration;

namespace EasyDapper.Infrastructure.Configurations
{

    public class EasyDapperCollection : ConfigurationElementCollection, IEasyDapperCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EasyDapperElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IEasyDapperElement) element).Key;
        }

        public new IEasyDapperElement this[string key]
        {
            get { return BaseGet(key) as IEasyDapperElement; }
        }
    }
}