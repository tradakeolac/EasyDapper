namespace EasyDapper.Infrastructure.Configurations
{
    using EasyDapper.Infrastructure.Extensions;

    public class EasyDapperConfiguration : IEasyDapperConfiguration
    {
        private readonly IEasyDapperSection EasyDapperSection;
        public EasyDapperConfiguration(IEasyDapperSection configSection)
        {
            this.EasyDapperSection = configSection;
        }

        public string CacheProvider
        {
            get
            {
                return EasyDapperSection.Settings["cacheProvider"].Value;
            }
        }

        public int DefaultExpiredCachingTime
        {
            get
            {
                return EasyDapperSection.Settings["defaultCachedExpiredTime"].Value.To<int>();
            }
        }

        public string ImplementedRepositoriesAssembly
        {
            get
            {
                return EasyDapperSection.Settings["implementedRepositoriesAssembly"].Value;
            }
        }

        public string SqlGeneratorProvider
        {
            get
            {
                return EasyDapperSection.Settings["sqlgeneratorProvider"].Value;
            }
        }
    }
}
