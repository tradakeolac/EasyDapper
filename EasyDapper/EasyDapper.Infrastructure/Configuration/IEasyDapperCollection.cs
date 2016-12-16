namespace EasyDapper.Infrastructure.Configurations
{
    public interface IEasyDapperCollection
    {
        IEasyDapperElement this[string key] { get; }
    }
}