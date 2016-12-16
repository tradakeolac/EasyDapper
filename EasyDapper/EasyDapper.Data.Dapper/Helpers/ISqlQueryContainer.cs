using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EasyDapper.Data.Criteria;

namespace EasyDapper.Data.Dapper.Helpers
{
    public interface ISqlQueryContainer
    {
        SqlQuery CountQuery<T>() where T : class;
        SqlQuery CountQuery<T>(Expression<Func<T, bool>> expression) where T : class;
        SqlQuery SelectById<T>(object id) where T : class;
        SqlQuery SelectQuery<T>() where T : class;
        SqlQuery SelectQuery<T>(Expression<Func<T, bool>> expression) where T : class;
        SqlQuery SelectQuery<T>(int page, int pageSize, IList<ISortCriteria> sorts = null) where T : class;
        SqlQuery SelectQuery<T>(Expression<Func<T, bool>> expression, int page, int pageSize, IList<ISortCriteria> sorts = null) where T : class;
        SqlQuery DeleteQuery<T>(Expression<Func<T, bool>> expression) where T : class;
        SqlQuery DeleteQuery<T>(T entity) where T : class;
        SqlQuery UpdateQuery<T>(T entity) where T : class;
        SqlQuery InsertQuery<T>(T entity) where T : class;
    }
}