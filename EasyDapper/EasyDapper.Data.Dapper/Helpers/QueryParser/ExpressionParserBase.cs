namespace EasyDapper.Data.Dapper.Helpers
{
    using EasyDapper.Data.Dapper.Extensions;
    using System;
    using System.Linq.Expressions;

    public abstract class ExpressionParserBase
    {
        protected string GetLinkOperator(ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    return "AND";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Default:
                    return string.Empty;
                default:
                    throw new ArgumentException("Not found matching type");
            }
        }


        protected virtual dynamic GetPropetyValue(MemberExpression expression)
        {
            return expression.ExtractPropertyValue();
        }
    }
}