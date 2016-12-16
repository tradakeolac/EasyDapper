namespace EasyDapper.Data.Dapper.Helpers
{
    using Infrastructure.Attributes;
    using System;
    using System.Reflection;
    using System.Linq;

    public class DefaultTableMetaDataProvider : ITableMetadataProvider
    {
        private const string TablePosfix = "s";

        public string PrimaryKey(Type type)
        {
            var tableMetadata = this.GetAnnotaion(type);
            return tableMetadata != null && tableMetadata.PrimaryKey != null
                ? tableMetadata.PrimaryKey
                : "Id";
        }

        public string PrimaryKey<TEntity>() where TEntity : class
        {
            return PrimaryKey(typeof(TEntity));
        }

        public string TableName(Type type)
        {
            var tableMetadata = this.GetAnnotaion(type);
            return tableMetadata != null && tableMetadata.Name != null
                ? tableMetadata.Name
                : string.Format("{0}{1}", type.Name, TablePosfix);
        }

        public string TableName<TEntity>() where TEntity : class
        {
            return TableName(typeof(TEntity));
        }

        private TableMappingAttribute GetAnnotaion(Type type)
        {
            return type.GetCustomAttribute<TableMappingAttribute>();
        }
    }
}