namespace EasyDapper.Data.Dapper.Helpers
{
    using System.Linq.Expressions;

    public interface IExpressionParserStrategy
    {
        void Parser(Expression expression, ExpressionType linkingType, ref QueryParameterCollection queryProperties);
    }
}