namespace EasyDapper.Data.Dapper.Helpers
{
    public class QueryParameter
    {
        public string LinkingOperator { get; set; }
        public string PropertyName { get; set; }
        public object ParamValue { get; set; }
        public string QueryOperator { get; set; }
        public string ParamName { get; set; }

        public QueryParameter(string linkingOperator, string propertyName, object propertyValue, string queryOperator)
        {
            this.LinkingOperator = linkingOperator;
            this.PropertyName = propertyName;
            this.ParamValue = propertyValue;
            this.QueryOperator = queryOperator;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} @{2} ", this.PropertyName, this.QueryOperator, this.ParamName);
        }
    }
}