namespace EasyDapper.Data.Dapper.Helpers.Configuration
{
    using System.Configuration;

    public class SqlGeneratorCollection : ConfigurationElementCollection, ISqlGeneratorCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SqlGeneratorElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ISqlGeneratorElement)element).ProviderName;
        }

        public new ISqlGeneratorElement this[string key]
        {
            get { return BaseGet(key) as ISqlGeneratorElement; }
        }

        [ConfigurationProperty("default")]
        public string DefaultProvider
        {
            get { return (string)base["default"]; }
            set { base["default"] = value; }
        }
    }
}
