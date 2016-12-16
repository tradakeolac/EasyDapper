namespace EasyDapper.Data.Specifications
{
    using System;
    using System.Linq.Expressions;

    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> Not();
        Expression<Func<T, bool>> ToExpression();
    }
}
