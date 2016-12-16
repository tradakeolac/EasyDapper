namespace EasyDapper.Data.Specifications
{
    using System;
    using System.Linq.Expressions;

    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T candidate);

        public abstract Expression<Func<T, bool>> ToExpression();

        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public ISpecification<T> Or(ISpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
}