
namespace EasyDapper.Data.Dapper.Helpers
{
    using System.Collections.Generic;
    using System.Text;
    using EasyDapper.Data.Criteria;

    public class SqlServer2012Generator : ISqlGenerator
    {
        protected ITableMetadataProvider TableInfo;

        public SqlServer2012Generator(ITableMetadataProvider tableInfo)
        {
            this.TableInfo = tableInfo;
        }

        public string GeneratePagedSelect<T>(int page, int pageSize, IList<ISortCriteria> sorts) where T : class
        {
            return this.GeneratePagedSelect<T>(page, pageSize, null);
        }

        public string GeneratePagedSelect<T>(int page, int pageSize, IList<ISortCriteria> sorts, string where) where T : class
        {
            var skip = (page - 1) * pageSize;
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("SELECT * FROM " + TableInfo.TableName<T>());

            // Where
            if (!string.IsNullOrEmpty(where))
                queryBuilder.Append(" WHERE " + where);

            queryBuilder.Append(" ORDER BY ");

            if (sorts != null && sorts.Count > 0)
            {                
                foreach (var sort in sorts)
                {
                    queryBuilder.Append(sort.PropertyName + " " + (sort.Desc ? "DESC" : ""));
                    if (sorts.IndexOf(sort) < (sorts.Count - 1))
                        queryBuilder.Append(", ");
                }
            }
            else
            {
                queryBuilder.Append(string.Format(" {0} ", TableInfo.PrimaryKey<T>()));
            }

            queryBuilder.Append(string.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", skip, pageSize));
            return queryBuilder.ToString();
        }
    }
}