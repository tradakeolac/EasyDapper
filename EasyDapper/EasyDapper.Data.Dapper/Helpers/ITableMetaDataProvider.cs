namespace EasyDapper.Data.Dapper.Helpers
{
    using System;

    public interface ITableMetadataProvider
    {
        string TableName<TEntity>() where TEntity : class;
        string TableName(Type type);
        string PrimaryKey<TEntity>() where TEntity : class;
        string PrimaryKey(Type type);
    }
}
