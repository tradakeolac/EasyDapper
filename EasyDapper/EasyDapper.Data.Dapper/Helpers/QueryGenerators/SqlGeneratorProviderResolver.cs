namespace EasyDapper.Data.Dapper.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EasyDapper.Infrastructure.Metadata;

    public class SqlGeneratorProviderResolver : ISqlGeneratorProviderResolver
    {
        readonly IEnumerable<Lazy<ISqlGenerator, ProviderKeyMetadata>> _services;

        public SqlGeneratorProviderResolver(IEnumerable<Lazy<ISqlGenerator, ProviderKeyMetadata>> services)
        {
            this._services = services;
        }

        public ISqlGenerator Resolve(string provider)
        {
            return this._services.FirstOrDefault(s => s.Metadata.Provider == provider).Value;
        }
    }
}
