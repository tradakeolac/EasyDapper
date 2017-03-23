namespace EasyDapper.Data.Dapper.Extensions
{
    using EasyDapper.Data.Dapper.Helpers;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ExpressionExtensions
    {
        public static dynamic ExtractPropertyValue(this MemberExpression expression)
        {
            // If the key is being added as a DB Parameter, then we have to also add the Parameter key/value pair to the collection
            // Because we're working off of Model Objects that should only contain Properties or Fields,
            // there should only be two options. PropertyInfo or FieldInfo... let's extract the VALUE accordingly
            var value = new object();
            if (expression.Member is PropertyInfo)
            {
                var exp = (MemberExpression)expression.Expression;
                var constant = (ConstantExpression)exp.Expression;
                var fieldInfoValue = ((FieldInfo)exp.Member).GetValue(constant.Value);
                value = ((PropertyInfo)expression.Member).GetValue(fieldInfoValue, null);

            }
            else if (expression.Member is FieldInfo)
            {
                var fieldInfo = expression.Member as FieldInfo;
                var constantExpression = expression.Expression as ConstantExpression;
                if (fieldInfo != null & constantExpression != null)
                {
                    value = fieldInfo.GetValue(constantExpression.Value);
                }
            }

            return value;
        }
    }
}
