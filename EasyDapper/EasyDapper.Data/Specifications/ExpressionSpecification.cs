
namespace EasyDapper.Data.Specifications
{
    using System;
    using System.Linq.Expressions;

    public class ExpressionSpecification<T> : ExpressionSpecificationBase<T>
    {
        readonly Expression<Func<T, bool>> expression;
        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expression = expression;
        }

        public override Expression<Func<T, bool>> Expression
        {
            get
            {
                return expression;
            }
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return this.Expression;
        }
    }
}