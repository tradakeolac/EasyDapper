namespace EasyDapper.Data.Dapper.Helpers.Configuration
{
    public interface ISqlGeneratorElement
    {
        string GeneratorType { get; set; }
        string ProviderName { get; set; }
    }
}