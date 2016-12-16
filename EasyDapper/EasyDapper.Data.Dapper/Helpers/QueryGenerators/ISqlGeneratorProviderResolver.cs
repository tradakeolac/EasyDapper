namespace EasyDapper.Data.Dapper.Helpers
{
    public interface ISqlGeneratorProviderResolver
    {
        ISqlGenerator Resolve(string provider);
    }
}