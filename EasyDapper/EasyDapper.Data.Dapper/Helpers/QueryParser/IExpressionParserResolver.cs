namespace EasyDapper.Data.Dapper.Helpers
{
    using System.Linq.Expressions;

    public interface IExpressionParserResolver
    {
        IExpressionParserStrategy Resolve(Expression expression);
    }
}