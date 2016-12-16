namespace EasyDapper.Infrastructure.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TableMappingAttribute : System.Attribute
    {
        public string Name { get; set; }
        public string PrimaryKey { get; set; }
    }
}