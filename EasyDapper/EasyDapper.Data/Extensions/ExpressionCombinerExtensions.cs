namespace EasyDapper.Data.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    static class ExpressionCombinerExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> newExp)
        {
            // get the visitor
            var visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());
            // replace the parameter in the expression just created
            newExp = visitor.Visit(newExp) as Expression<Func<T, bool>>;

            // now you can and together the two expressions
            var binExp = Expression.AndAlso(exp.Body, newExp.Body);
            // and return a new lambda, that will do what you want. NOTE that the binExp has reference only to te newExp.Parameters[0] (there is only 1) parameter, and no other
            return Expression.Lambda<Func<T, bool>>(binExp, newExp.Parameters);
        }

        public static Expression<Func<T, bool>> OrAlso<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> newExp)
        {
            // get the visitor
            var visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());
            // replace the parameter in the expression just created
            newExp = visitor.Visit(newExp) as Expression<Func<T, bool>>;

            // now you can and together the two expressions
            var binExp = Expression.OrElse(exp.Body, newExp.Body);
            // and return a new lambda, that will do what you want. NOTE that the binExp has reference only to te newExp.Parameters[0] (there is only 1) parameter, and no other
            return Expression.Lambda<Func<T, bool>>(binExp, newExp.Parameters);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> exp)
        {
            // get the visitor
            var visitor = new ParameterUpdateVisitor(exp.Parameters.First(), exp.Parameters.First());
            // replace the parameter in the expression just created
            exp = visitor.Visit(exp) as Expression<Func<T, bool>>;

            // now you can and together the two expressions
            var binExp = Expression.Not(exp.Body);

            // and return a new lambda, that will do what you want. NOTE that the binExp has reference only to te newExp.Parameters[0] (there is only 1) parameter, and no other
            return Expression.Lambda<Func<T, bool>>(binExp, exp.Parameters);
        }

        class ParameterUpdateVisitor : ExpressionVisitor
        {
            private ParameterExpression _oldParameter;
            private ParameterExpression _newParameter;

            public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (object.ReferenceEquals(node, _oldParameter))
                    return _newParameter;

                return base.VisitParameter(node);
            }
        }
    }
}
