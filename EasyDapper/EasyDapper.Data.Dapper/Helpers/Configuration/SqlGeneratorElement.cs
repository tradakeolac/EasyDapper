namespace EasyDapper.Data.Dapper.Helpers.Configuration
{
    using System.Configuration;

    public class SqlGeneratorElement : ConfigurationElement, ISqlGeneratorElement
    {
        [ConfigurationProperty("providerName")]
        public string ProviderName
        {
            get { return (string)this["providerName"]; }
            set { this["providerName"] = value; }
        }

        [ConfigurationProperty("generatorType")]
        public string GeneratorType
        {
            get { return (string)this["generatorType"]; }
            set { this["generatorType"] = value; }
        }
    }
}
