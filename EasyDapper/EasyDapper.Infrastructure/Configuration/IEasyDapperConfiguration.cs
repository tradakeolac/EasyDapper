namespace EasyDapper.Infrastructure.Configurations
{
    public interface IEasyDapperConfiguration
    {
        string SqlGeneratorProvider { get;}
        string ImplementedRepositoriesAssembly { get; }
        string CacheProvider { get; }
        int DefaultExpiredCachingTime { get; }
    }
}
