namespace EasyDapper.Data.Dapper.Helpers.Configuration
{
    using System.Collections;

    public interface ISqlGeneratorCollection : IEnumerable
    {
        ISqlGeneratorElement this[string key] { get; }
        string DefaultProvider { get; set; }
    }
}