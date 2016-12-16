namespace EasyDapper.Data.Dapper
{
    using System;
    using System.Data;
    using System.Linq.Expressions;
    using Extensions;
    using Repositories.Abstractions;
    using Specifications;
    using Dapper;
    using Helpers;
    using global::Dapper;


    /// <summary> 
    /// Generic repository 
    /// </summary> 
    public class GenericRepository : DataLoaderRepository, IRepository
    {
        public GenericRepository(IDbConnection dbConnection, ISqlQueryContainer dynamicQueryHelper)
            : base(dbConnection, dynamicQueryHelper)
        {
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            var query = QueryBuilder.InsertQuery(entity);
            this.DbConnection.Run(db => db.Execute(query.Sql, query.Params));
        }
        
        public void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            this.DbConnection.Run(db =>
            {
                var deleteQuery = QueryBuilder.DeleteQuery(criteria.ToExpression());
                db.ExecuteScalar(deleteQuery.Sql, deleteQuery.Params);
            });
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            this.DbConnection.Run(db =>
            {
                var deleteQuery = QueryBuilder.DeleteQuery(criteria);
                db.ExecuteScalar(deleteQuery.Sql, deleteQuery.Params);
            });
        }
        
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            this.DbConnection.Run(db =>
            {
                var deleteQuery = QueryBuilder.DeleteQuery(entity);
                db.ExecuteScalar(deleteQuery.Sql, deleteQuery.Params);
            });
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {            
            this.DbConnection.Run(db =>
            {
                var updateQuery = QueryBuilder.UpdateQuery(entity);
                db.ExecuteScalar(updateQuery.Sql, updateQuery.Params);
            });
        }
    }
}