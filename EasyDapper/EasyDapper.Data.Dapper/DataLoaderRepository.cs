namespace EasyDapper.Data.Dapper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq.Expressions;
    using EasyDapper.Data.Repositories.Abstractions;
    using EasyDapper.Data.Specifications;
    using EasyDapper.Data.Dapper.Extensions;
    using EasyDapper.Data.Dapper.Helpers;
    using global::Dapper;
    using EasyDapper.Data.Criteria;

    public class DataLoaderRepository : IDataLoaderRepository
    {
        protected readonly IDbConnection DbConnection;
        protected readonly ISqlQueryContainer QueryBuilder;

        public DataLoaderRepository(IDbConnection dbConnection, ISqlQueryContainer queryBuilder)
        {
            this.DbConnection = dbConnection;
            this.QueryBuilder = queryBuilder;
        }

        public int Count<TEntity>() where TEntity : class
        {
            return this.DbConnection.Run(db => (int)db.ExecuteScalar(QueryBuilder.CountQuery<TEntity>().Sql));
        }

        public int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.CountQuery(criteria.ToExpression());
                return (int)db.ExecuteScalar(query.Sql, query.Params);
            });
        }

        public int Count<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.CountQuery(criteria);
                return (int)db.ExecuteScalar(query.Sql, query.Params);
            });
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectQuery(criteria);
                return db.Query<TEntity>(query.Sql, query.Params);
            });
        }

        public IEnumerable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectQuery(criteria.ToExpression());
                return db.Query<TEntity>(query.Sql, query.Params);
            });
        }

        public TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectQuery(criteria);
                return db.QueryFirstOrDefault<TEntity>(query.Sql, query.Params);
            });
        }

        public TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectQuery(criteria.ToExpression());
                return db.QueryFirstOrDefault<TEntity>(query.Sql, query.Params);
            });
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectQuery<TEntity>();
                return db.Query<TEntity>(query.Sql);
            });
        }

        public IEnumerable<TEntity> Get<TEntity>(ISpecification<TEntity> criteria, int page, int pageSize) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectQuery<TEntity>(criteria.ToExpression(), page, pageSize);
                return db.Query<TEntity>(query.Sql, query.Params);
            });
        }

        public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> criteria, int page, int pageSize) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectQuery<TEntity>(criteria, page, pageSize);
                return db.Query<TEntity>(query.Sql, query.Params);
            });
        }

        public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> criteria, IList<ISortCriteria> sorts, int page, int pageSize) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectQuery<TEntity>(criteria, page, pageSize, sorts);
                return db.Query<TEntity>(query.Sql, query.Params);
            });
        }

        public IEnumerable<TEntity> Get<TEntity>(ISpecification<TEntity> criteria, IList<ISortCriteria> sorts, int page, int pageSize) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectQuery(criteria.ToExpression(), page, pageSize, sorts);
                return db.Query<TEntity>(query.Sql, query.Params);
            });
        }
        
        public TEntity GetById<TEntity>(object id) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                var query = QueryBuilder.SelectById<TEntity>(id);
                return db.QueryFirstOrDefault<TEntity>(query.Sql, query.Params);
            });
        }

        protected virtual IEnumerable<TEntity> ExecuteStoreProcedure<TEntity>(string storeProcedureName, dynamic parameters) where TEntity : class
        {
            return this.DbConnection.Run(db =>
            {
                return db.Query<TEntity>(storeProcedureName, param: (object)parameters, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
