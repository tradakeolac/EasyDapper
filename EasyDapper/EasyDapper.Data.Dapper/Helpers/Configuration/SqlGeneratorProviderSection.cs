namespace EasyDapper.Data.Dapper.Helpers.Configuration
{
    using System.Configuration;

    public class SqlGeneratorProviderSection : ConfigurationSection, ISqlGeneratorProviderSection
    {
        [ConfigurationProperty("generators", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(SqlGeneratorCollection))]
        public SqlGeneratorCollection SqlGenerators
        {
            get { return (SqlGeneratorCollection)base["generators"]; }
        }

        [ConfigurationProperty("default")]
        public string Default
        {
            get { return (string)base["default"]; }
            set { base["default"] = value; }
        }
    }
}
