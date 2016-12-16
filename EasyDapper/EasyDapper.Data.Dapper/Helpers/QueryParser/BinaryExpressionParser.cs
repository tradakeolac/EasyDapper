namespace EasyDapper.Data.Dapper.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class BinaryExpressionParser : ExpressionParserBase, IExpressionParserStrategy
    {
        protected readonly IExpressionParserResolver Resolver;

        public BinaryExpressionParser(IExpressionParserResolver resolver)
        {
            this.Resolver = resolver;
        }

        public void Parser(Expression expression, ExpressionType linkingType, ref QueryParameterCollection queryProperties)
        {
            var binary = (BinaryExpression)expression;
            if (expression.NodeType != ExpressionType.AndAlso && expression.NodeType != ExpressionType.OrElse)
            {
                var propertyName = GetPropertyName(binary);
                dynamic propertyValue = GetPropetyValue(binary.Right as MemberExpression);
                var opr = GetOperator(expression.NodeType);
                var link = GetLinkOperator(linkingType);

                queryProperties.Add(new QueryParameter(link, propertyName, propertyValue, opr));
            }
            else
            {
                Resolver.Resolve(binary.Left).Parser(binary.Left, expression.NodeType, ref queryProperties);
                Resolver.Resolve(binary.Right).Parser(binary.Right, expression.NodeType, ref queryProperties);
            }
        }

        private string GetPropertyName(BinaryExpression body)
        {
            string propertyName = body.Left.ToString().Split(new char[] { '.' })[1];

            if (body.Left.NodeType == ExpressionType.Convert)
            {
                // hack to remove the trailing ) when convering.
                propertyName = propertyName.Replace(")", string.Empty);
            }

            return propertyName;
        }

        private string GetOperator(ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "!=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.GreaterThan:
                    return ">";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}