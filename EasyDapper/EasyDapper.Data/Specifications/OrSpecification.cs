namespace EasyDapper.Data.Specifications
{
    using System;
    using System.Linq.Expressions;
    using EasyDapper.Data.Extensions;

    public class OrSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this._left = left;
            this._right = right;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return this.ToExpression().Compile().Invoke(candidate);
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return _left.ToExpression().OrAlso(_right.ToExpression());
        }
    }
}