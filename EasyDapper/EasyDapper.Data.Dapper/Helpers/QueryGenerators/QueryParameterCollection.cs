using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;

namespace EasyDapper.Data.Dapper.Helpers
{

    public class QueryParameterCollection : IList<QueryParameter>
    {
        private IList<QueryParameter> subQueryParams = new List<QueryParameter>();
        private const string ParamPrefix = "p";
        public QueryParameter this[int index]
        {
            get { return subQueryParams[index]; }
            set { subQueryParams[index] = value; }
        }

        public int Count { get { return subQueryParams.Count; } }

        public bool IsReadOnly { get { return false; } }

        public void Add(QueryParameter item)
        {
            item.ParamName = ParamPrefix + this.Count;
            subQueryParams.Add(item);
        }

        public void Clear()
        {
            subQueryParams.Clear();
        }

        public bool Contains(QueryParameter item)
        {
            return subQueryParams.Contains(item);
        }

        public void CopyTo(QueryParameter[] array, int arrayIndex)
        {
            subQueryParams.CopyTo(array, arrayIndex);
        }

        public IEnumerator<QueryParameter> GetEnumerator()
        {
            return subQueryParams.GetEnumerator();
        }

        public int IndexOf(QueryParameter item)
        {
            return subQueryParams.IndexOf(item);
        }

        public void Insert(int index, QueryParameter item)
        {
            subQueryParams.Insert(index, item);
        }

        public bool Remove(QueryParameter item)
        {
            return subQueryParams.Remove(item);
        }

        public void RemoveAt(int index)
        {
            subQueryParams.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return subQueryParams.GetEnumerator();
        }

        public SqlQuery ToQuery()
        {
            var builder = new StringBuilder();
            IDictionary<string, object> expando = new ExpandoObject();
            for (int i = 0; i < this.Count; i++)
            {
                QueryParameter item = this[i];

                if (!string.IsNullOrEmpty(item.LinkingOperator) && i > 0)
                {
                    builder.Append(string.Format("{0} {1} {2} @{3} ", item.LinkingOperator, item.PropertyName,
                                                 item.QueryOperator, item.ParamName));
                }
                else
                {
                    builder.Append(string.Format("{0} {1} @{2} ", item.PropertyName, item.QueryOperator, item.ParamName));
                }

                expando[item.ParamName] = item.ParamValue;
            }

            return new SqlQuery(builder.ToString(), expando);
        }
    }
}