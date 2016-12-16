namespace EasyDapper.Data.Dapper.Helpers
{
    using System.Collections;
    using System.Linq.Expressions;
    using System.Reflection;

    public class MethodCallExpressionParser : ExpressionParserBase, IExpressionParserStrategy
    {
        public void Parser(Expression expression, ExpressionType linkingType, ref QueryParameterCollection queryProperties)
        {
            var methodCallExpression = (MethodCallExpression)expression;
            var propertyName = GetPropertyName(methodCallExpression);
            string propertyValue = MethodCallToSqlArgument(methodCallExpression);
            var opr = MethodCallToSqlOperand(methodCallExpression);
            var link = GetLinkOperator(linkingType);

            if (!string.IsNullOrEmpty(propertyName) &&
                !string.IsNullOrEmpty(propertyValue) &&
                !string.IsNullOrEmpty(opr))
            {
                queryProperties.Add(new QueryParameter(link, propertyName, propertyValue, opr));
            }
        }

        private string GetPropertyName(MethodCallExpression body)
        {
            string propertyName = null;
            if (body.Object != null) // Current entity call its properties
                propertyName = body.Object.ToString().Split(new char[] { '.' })[1];
            else
                propertyName = body.Arguments[1].ToString().Split(new char[] { '.' })[1];

            return propertyName;
        }

        private string MethodCallToSqlOperand(MethodCallExpression expression)
        {
            if (expression.Method.DeclaringType == typeof(string))
            {
                return @"LIKE";
            }

            if (expression.Method.DeclaringType == typeof(System.Linq.Enumerable))
            {
                return @"IN";
            }

            return @"LIKE";
        }

        private dynamic MethodCallToSqlArgument(MethodCallExpression expression)
        {
            dynamic value = expression.Arguments[0];

            if (expression.Method.DeclaringType == typeof(string))
            {
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
            }

            if (expression.Method.DeclaringType == typeof(System.Linq.Enumerable))
            {
                if (value is MemberExpression)
                {
                    value = GetPropetyValue(value as MemberExpression) as IEnumerable;
                }

                return $"({string.Join(",", value)})";
            }

            return null;
        }
    }
}