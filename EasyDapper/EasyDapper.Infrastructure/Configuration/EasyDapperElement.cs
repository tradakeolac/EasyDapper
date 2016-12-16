using System.Configuration;

namespace EasyDapper.Infrastructure.Configurations
{
    public class EasyDapperElement : ConfigurationElement, IEasyDapperElement
    {
        [ConfigurationProperty("key")]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}