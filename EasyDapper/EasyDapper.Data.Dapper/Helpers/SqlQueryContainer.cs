namespace EasyDapper.Data.Dapper.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Criteria;
    using Infrastructure.Configurations;
    using System.Reflection;
    using Attributes;

    /// <summary>
    /// Helper class provides methodology to build SQL query
    /// </summary>
    public class SqlQueryContainer : ISqlQueryContainer
    {
        protected readonly ITableMetadataProvider TableInfo;
        protected readonly ISqlGeneratorProviderResolver SqlGeneratorProviderResolver;
        protected readonly IEasyDapperConfiguration Configuration;
        protected readonly IExpressionParserResolver ExpressionParserResolver;

        protected virtual ISqlGenerator SqlGenerator
        {
            get { return this.SqlGeneratorProviderResolver.Resolve(Configuration.SqlGeneratorProvider); }
        }

        public SqlQueryContainer(ITableMetadataProvider tableBuilder,
            ISqlGeneratorProviderResolver resolver, IEasyDapperConfiguration configuration,
            IExpressionParserResolver expressionParserResolver)
        {
            this.TableInfo = tableBuilder;
            this.SqlGeneratorProviderResolver = resolver;
            this.Configuration = configuration;
            this.ExpressionParserResolver = expressionParserResolver;
        }

        public SqlQuery SelectById<T>(object id) where T : class
        {
            IDictionary<string, object> param = new ExpandoObject();
            param.Add("@ID", id);
            return new SqlQuery(string.Format("SELECT * FROM {0} WHERE {1} = @{1}", TableInfo.TableName<T>(), TableInfo.PrimaryKey<T>()), param);
        }

        public SqlQuery SelectQuery<T>() where T : class
        {
            return SelectQuery<T>(null);
        }

        public SqlQuery SelectQuery<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var builder = new StringBuilder();

            // convert the query parms into a SQL string and dynamic property object
            builder.Append("SELECT * FROM ");
            builder.Append(TableInfo.TableName<T>());

            if (expression != null)
            {
                var whereCondition = WhereQuery(expression);
                if (!string.IsNullOrEmpty(whereCondition.Sql))
                {
                    builder.Append(" WHERE ");
                    builder.Append(whereCondition.Sql);

                    return new SqlQuery(builder.ToString(), whereCondition.Params);
                }

                return new SqlQuery(builder.ToString());
            }
            else
            {
                return new SqlQuery(builder.ToString());
            }
        }

        public SqlQuery SelectQuery<T>(int page, int pageSize, IList<ISortCriteria> sorts = null) where T : class
        {
            return new SqlQuery(SqlGenerator.GeneratePagedSelect<T>(page, pageSize, sorts));
        }

        public SqlQuery SelectQuery<T>(Expression<Func<T, bool>> expression, int page, int pageSize, IList<ISortCriteria> sorts = null) where T : class
        {
            var whereCondition = WhereQuery(expression);
            if (whereCondition != null)
            {
                return new SqlQuery(SqlGenerator.GeneratePagedSelect<T>(page, pageSize, sorts, whereCondition.Sql), whereCondition.Params);
            }

            return new SqlQuery(SqlGenerator.GeneratePagedSelect<T>(page, pageSize, sorts));
        }

        public SqlQuery DeleteQuery<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var whereQuery = WhereQuery(expression);
            var query = new StringBuilder();
            query.Append(string.Format("DELETE FROM {0} ", TableInfo.TableName<T>()));
            query.Append(" WHERE ");
            query.Append(whereQuery.Sql);

            return new SqlQuery(query.ToString(), whereQuery);
        }

        public SqlQuery InsertQuery<T>(T entity) where T : class
        {
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var prop in CachedProperty.GetPropertiesExcluded<T>(TableInfo.PrimaryKey<T>()))
            {
                expando[prop.Name] = prop.GetValue(entity);
            }

            var columns = CachedProperty.GetPropertyNamesExcluded<T>(TableInfo.PrimaryKey<T>());

            var query = string.Format("INSERT INTO {0} ({1}) VALUES (@{2})",
                                 TableInfo.TableName<T>(),
                                 string.Join(",", columns),
                                 string.Join(",@", columns));

            return new SqlQuery(query, expando);
        }

        public SqlQuery DeleteQuery<T>(T entity) where T : class
        {
            var query = string.Format("DELETE FROM {0} WHERE {1} = @{1}", TableInfo.TableName<T>(), TableInfo.PrimaryKey<T>());
            IDictionary<string, object> expando = new ExpandoObject();
            expando.Add($"@{TableInfo.PrimaryKey<T>()}", expando);

            return new SqlQuery(query, expando);
        }

        public SqlQuery UpdateQuery<T>(T entity) where T : class
        {
            var idKey = TableInfo.PrimaryKey<T>();
            var propertiesExcluedPrimaryKey = CachedProperty.GetProperties<T>();

            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var prop in propertiesExcluedPrimaryKey)
            {
                expando[prop.Name] = prop.GetValue(entity);
            }

            var updateProperties = propertiesExcluedPrimaryKey.Where(p => p.Name != idKey).Select(prop => prop.Name + "=@" + prop.Name).ToList();

            var query = string.Format("UPDATE {0} SET {1} WHERE {2}=@{2}", TableInfo.TableName<T>(), string.Join(",", updateProperties), TableInfo.PrimaryKey<T>());
            return new SqlQuery(query, expando);
        }

        public SqlQuery CountQuery<T>() where T : class
        {
            return CountQuery<T>(null);
        }

        public SqlQuery CountQuery<T>(Expression<Func<T, bool>> expression) where T : class
        {
            StringBuilder query = new StringBuilder();
            query.Append(string.Format("COUNT {0} FROM {1} ", TableInfo.PrimaryKey<T>(), TableInfo.TableName<T>()));
            if (expression != null)
            {
                var whereQuery = WhereQuery(expression);
                query.Append(" WHERE ");
                query.Append(whereQuery.Sql);
                return new SqlQuery(query.ToString(), whereQuery);
            }

            return new SqlQuery(query.ToString());
        }

        #region Private and sub methods

        protected virtual SqlQuery WhereQuery<T>(Expression<Func<T, bool>> expression) where T : class
        {
            var queryProperties = new QueryParameterCollection();

            // walk the tree and build up a list of query parameter objects
            // from the left and right branches of the expression tree
            ExpressionParserResolver.Resolve(expression.Body).Parser(expression.Body, ExpressionType.Default, ref queryProperties);

            return queryProperties.ToQuery();
        }

        #endregion

        private static class CachedProperty
        {
            private static readonly Dictionary<Type, string[]> _cachedPropertyNames = new Dictionary<Type, string[]>();
            private static readonly Dictionary<Type, PropertyInfo[]> _cachedProperties = new Dictionary<Type, PropertyInfo[]>();

            public static string[] GetPropertyNames<T>() where T : class
            {
                if (!_cachedPropertyNames.ContainsKey(typeof(T)))
                    _cachedPropertyNames.Add(typeof(T), GetProperties<T>().Select(p =>
                    {
                        var metaPro = p.GetCustomAttribute<MapColumnAttribute>();
                        return metaPro != null ? metaPro.Column : p.Name;
                    }).ToArray());

                return _cachedPropertyNames[typeof(T)];
            }

            public static PropertyInfo[] GetProperties<T>() where T : class
            {
                if (!_cachedProperties.ContainsKey(typeof(T)))
                {
                    var properties = typeof(T).GetProperties().Where(p => p.GetCustomAttribute<IgnoreMapAttribute>() == null);
                    _cachedProperties.Add(typeof(T), properties.ToArray());
                }

                return _cachedProperties[typeof(T)];
            }

            public static string[] GetPropertyNamesExcluded<T>(string excludedProperty) where T : class
            {
                return GetPropertyNames<T>().Where(p => p != excludedProperty).ToArray();
            }


            public static PropertyInfo[] GetPropertiesExcluded<T>(string excludedProperty) where T : class
            {
                return GetProperties<T>().Where(p => p.Name != excludedProperty).ToArray();
            }
        }
    }
}
