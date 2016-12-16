namespace EasyDapper.Data.Dapper.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using EasyDapper.Infrastructure.Metadata;

    public class ExpressionParserResovler : IExpressionParserResolver
    {
        protected readonly IEnumerable<Lazy<IExpressionParserStrategy, ProviderTypeMetadata>> _parserServices;

        public ExpressionParserResovler(IEnumerable<Lazy<IExpressionParserStrategy, ProviderTypeMetadata>> services)
        {
            this._parserServices = services;
        }

        public IExpressionParserStrategy Resolve(Expression expression)
        {
            var service = this._parserServices.FirstOrDefault(s => s.Metadata.Provider == expression.GetType());
            if (service == null)
                service = this._parserServices.FirstOrDefault(s => expression.GetType().IsSubclassOf(s.Metadata.Provider));
            return service != null ? service.Value : null;
        }
    }
}