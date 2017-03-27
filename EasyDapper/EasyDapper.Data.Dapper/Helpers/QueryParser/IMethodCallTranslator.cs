namespace EasyDapper.Data.Dapper.Helpers.QueryParser
{
    using EasyDapper.Data.Dapper.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IMethodCallTranslator
    {
        dynamic TranslateQueryArgument(MethodCallExpression expression);
        string TranslateQueryOperand(MethodCallExpression expression);
    }
    
    public class StringMethodCallTranslator : IMethodCallTranslator
    {
        public dynamic TranslateQueryArgument(MethodCallExpression expression)
        {
            dynamic value = expression.Arguments[0];

            if (expression.Method.Name == @"Contains")
            {
                return string.Format(@"%{0}%", value.Value);
            }

            if (expression.Method.Name == @"StartsWith")
            {
                return string.Format(@"{0}%", value.Value);
            }

            if (expression.Method.Name == @"EndsWith")
            {
                return string.Format(@"%{0}", value.Value);
            }

            throw new InvalidOperationException();
        }

        public string TranslateQueryOperand(MethodCallExpression expression)
        {
            return @"LIKE";
        }
    }

    public class IteratorMethodCallTranslator : IMethodCallTranslator
    {
        public dynamic TranslateQueryArgument(MethodCallExpression expression)
        {
            return @"IN";
        }

        public string TranslateQueryOperand(MethodCallExpression expression)
        {
            var value = (expression.Arguments[0] as MemberExpression).ExtractPropertyValue() as IEnumerable;

            return $"({string.Join(",", value)})";
        }
    }

    public interface IServiceResolver<TService>
        where TService : class
    {
        TService Resolve(string key);
        TService Resolve(Type key);
    }

    public class DefaultServiceResolver<TService> : IServiceResolver<TService>
        where TService : class
    {

        protected readonly IEnumerable<Lazy<TService, Infrastructure.Metadata.ProviderTypeMetadata>> Services;
        protected readonly IEnumerable<Lazy<TService, Infrastructure.Metadata.ProviderKeyMetadata>> ServicesStringKey;

        public DefaultServiceResolver(IEnumerable<Lazy<TService, Infrastructure.Metadata.ProviderTypeMetadata>> services,
            IEnumerable<Lazy<TService, Infrastructure.Metadata.ProviderKeyMetadata>> serviceStringKey) 
        {
            this.Services = services;
            this.ServicesStringKey = serviceStringKey;
        }

        public TService Resolve(string key)
        {
            var service = this.ServicesStringKey.FirstOrDefault(s => s.Metadata.Provider == key);
            return service?.Value;
        }

        public TService Resolve(Type key)
        {
            var service = this.Services.FirstOrDefault(s => s.Metadata.Provider == key);

            return service?.Value;
        }
    }
}
