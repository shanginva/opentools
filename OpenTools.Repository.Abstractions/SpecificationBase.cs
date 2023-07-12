using System.Linq.Expressions;

namespace OpenTools.Repository.Abstractions;

public class All<TParam> : SpecificationBase<TParam>
{
    public All() : base(_ => true)
    {
    }
}

public abstract class SpecificationBase<TParam>
{
    public Expression<Func<TParam, bool>> Expression { get; }

    protected SpecificationBase(Expression<Func<TParam, bool>> expression)
    {
        Expression = expression;
    }

    public virtual bool IsSatisfiedBy(TParam param)
        => Expression
            .Compile()
            .Invoke(param);

    public SpecificationBase<TParam> And(SpecificationBase<TParam> specification)
        => new AndSpecification<TParam>(this, specification);

    public SpecificationBase<TParam> Or(SpecificationBase<TParam> specification)
        => new OrSpecification<TParam>(this, specification);

    public SpecificationBase<TParam> All()
    => new All<TParam>();

    private class AndSpecification<T> : SpecificationBase<T>
    {
        public AndSpecification(SpecificationBase<T> left, SpecificationBase<T> right)
            : base(CombineSpecifications(left, right)) { }

        private static Expression<Func<T, bool>> CombineSpecifications(
            SpecificationBase<T> left,
            SpecificationBase<T> right)
        {
            var invokedExpression = System.Linq.Expressions.Expression
                .Invoke(right.Expression, left.Expression.Parameters);
            var andExpression = System.Linq.Expressions.Expression
                .AndAlso(left.Expression.Body, invokedExpression);
            return System.Linq.Expressions.Expression
                .Lambda<Func<T, bool>>(andExpression, left.Expression.Parameters);
        }
    }

    private class OrSpecification<T> : SpecificationBase<TParam>
    {
        public OrSpecification(SpecificationBase<TParam> specification1, SpecificationBase<TParam> specification2)
            : base(CombineSpecifications(specification1, specification2)) { }

        private static Expression<Func<TParam, bool>> CombineSpecifications(
            SpecificationBase<TParam> left,
            SpecificationBase<TParam> right)
        {
            var invocedSpecification = System.Linq.Expressions.Expression
                .Invoke(right.Expression, left.Expression.Parameters);
            var orExpression = System.Linq.Expressions.Expression
                .Or(left.Expression.Body, invocedSpecification);
            return System.Linq.Expressions.Expression
                .Lambda<Func<TParam, bool>>(orExpression, left.Expression.Parameters);
        }
    }
}