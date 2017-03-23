using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EasyDapper.Data.Dapper.Queries
{
    public static class QueryBuilderExtensions
    {
        public static IncludeReference<TModel> Include<TModel>(this IncludeReference<TModel> builder, Expression<Func<TModel, object>> expression) 
            where TModel : class
        {
            if (expression.Body is MemberExpression)
            {
                var type = expression.Body.Type;
                var refType = type.IsGenericType ? type.GetGenericArguments()[0] : type;
            }

            return builder;
        }        
    }

    public class DelegateQueryBuilder<T> 
    {
        
    }

    public class IncludeReference<T> : DelegateQueryBuilder<T>
    {
        public IncludeReference()
        {
            this.References = new List<ReferenceMetadata>();
        }
        public IList<ReferenceMetadata> References { get; set; }
    }

    public class ReferenceMetadata
    {
        public string ForeignKey { get; set; }
        public Type ReferenceType { get; set; }
    }
}
