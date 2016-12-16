namespace EasyDapper.Infrastructure.Configurations
{
    using System.Configuration;

    public class EasyDapperSection : ConfigurationSection, IEasyDapperSection
    {
        [ConfigurationProperty("settings", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(EasyDapperCollection))]
        public EasyDapperCollection Settings
        {
            get { return (EasyDapperCollection)base["settings"]; }
        }
    }
}