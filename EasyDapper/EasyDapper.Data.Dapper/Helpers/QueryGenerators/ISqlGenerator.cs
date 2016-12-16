namespace EasyDapper.Data.Dapper.Helpers
{
    using System.Collections.Generic;
    using EasyDapper.Data.Criteria;

    public interface ISqlGenerator
    {
        string GeneratePagedSelect<T>(int page, int pageSize, IList<ISortCriteria> sorts) where T : class;
        string GeneratePagedSelect<T>(int page, int pageSize, IList<ISortCriteria> sorts, string where) where T : class;
    }
}
