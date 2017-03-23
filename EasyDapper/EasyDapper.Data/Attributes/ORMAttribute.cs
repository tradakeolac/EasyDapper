namespace EasyDapper.Data.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public abstract class ORMAttribute : System.Attribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public sealed class IgnoreMapAttribute : ORMAttribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MapColumnAttribute : ORMAttribute
    {
        public MapColumnAttribute(string property)
        {
            Column = property;
        }

        public string Column { get; set; }
    }
}
