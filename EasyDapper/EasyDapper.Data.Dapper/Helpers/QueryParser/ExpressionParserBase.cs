namespace EasyDapper.Data.Dapper.Helpers
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

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


        protected dynamic GetPropetyValue(MemberExpression expression)
        {

            // If the key is being added as a DB Parameter, then we have to also add the Parameter key/value pair to the collection
            // Because we're working off of Model Objects that should only contain Properties or Fields,
            // there should only be two options. PropertyInfo or FieldInfo... let's extract the VALUE accordingly
            var value = new object();
            if ((expression.Member as PropertyInfo) != null)
            {
                var exp = (MemberExpression)expression.Expression;
                var constant = (ConstantExpression)exp.Expression;
                var fieldInfoValue = ((FieldInfo)exp.Member).GetValue(constant.Value);
                value = ((PropertyInfo)expression.Member).GetValue(fieldInfoValue, null);

            }
            else if ((expression.Member as FieldInfo) != null)
            {
                var fieldInfo = expression.Member as FieldInfo;
                var constantExpression = expression.Expression as ConstantExpression;
                if (fieldInfo != null & constantExpression != null)
                {
                    value = fieldInfo.GetValue(constantExpression.Value);
                }
            }
            else
            {
                throw new InvalidMemberException();
            }

            return value;
        }

    }
}