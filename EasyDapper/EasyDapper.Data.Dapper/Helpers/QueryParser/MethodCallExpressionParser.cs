namespace EasyDapper.Data.Dapper.Helpers
{
    using EasyDapper.Data.Dapper.Helpers.QueryParser;
    using System.Collections;
    using System.Linq.Expressions;
    using System.Reflection;

    public class MethodCallExpressionParser : ExpressionParserBase, IExpressionParserStrategy
    {
        protected readonly IServiceResolver<IMethodCallTranslator> ServiceResolver;

        public MethodCallExpressionParser(IServiceResolver<IMethodCallTranslator> serviceResolver)
        {
            this.ServiceResolver = serviceResolver;
        }

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

        private static string GetPropertyName(MethodCallExpression body)
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
            return ServiceResolver.Resolve(expression.Method.DeclaringType).TranslateQueryOperand(expression);
        }

        private dynamic MethodCallToSqlArgument(MethodCallExpression expression)
        {
            return ServiceResolver.Resolve(expression.Method.DeclaringType).TranslateQueryArgument(expression);
        }
    }
}