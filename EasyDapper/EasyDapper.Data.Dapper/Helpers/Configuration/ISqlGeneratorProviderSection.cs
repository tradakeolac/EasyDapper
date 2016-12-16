namespace EasyDapper.Data.Dapper.Helpers.Configuration
{
    public interface ISqlGeneratorProviderSection
    {
        string Default { get; set; }
        SqlGeneratorCollection SqlGenerators { get; }
    }
}