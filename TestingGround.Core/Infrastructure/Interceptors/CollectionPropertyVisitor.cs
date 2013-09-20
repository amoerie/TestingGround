using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingGround.Core.Domain.Internal.Bases;
using TestingGround.Core.Domain.Internal.Contracts;

namespace TestingGround.Core.Infrastructure.Interceptors
{
    public class CollectionPropertyVisitor<TProperty>: ExpressionVisitor where TProperty: Entity
    {
        public Expression<Func<TProperty, bool>> Filter { get; set; }

        public CollectionPropertyVisitor()
        {
            Filter = entity => !entity.Deleted;
        }

        protected override Expression VisitMember(MemberExpression ex)
        {
            return !ex.Type.IsGenericType ? base.VisitMember(ex) : CreateWhereExpression(Filter, ex) ?? base.VisitMember(ex);
        }

        private Expression CreateWhereExpression(Expression<Func<TProperty, bool>> filter, MemberExpression memberExpression)
        {
            var type = memberExpression.Type.GetGenericArguments().First();
            var test = CreateExpression(filter, type);
            if (test == null)
                return null;
            var listType = typeof(ICollection<>).MakeGenericType(type);
            return Expression.Convert(Expression.Call(typeof(Enumerable), "Where", new[] { type }, memberExpression, test), listType);
        }

        private LambdaExpression CreateExpression(Expression<Func<TProperty, bool>> condition, Type type)
        {
            var lambda = (LambdaExpression) condition;
            if (!typeof(TProperty).IsAssignableFrom(type))
                return null;

            var newParams = new[] { Expression.Parameter(type, "entity") };
            var paramMap = lambda.Parameters.Select((original, i) => new { original, replacement = newParams[i] }).ToDictionary(p => p.original, p => p.replacement);
            var fixedBody = ParameterRebinder.ReplaceParameters(paramMap, lambda.Body);
            lambda = Expression.Lambda(fixedBody, newParams);

            return lambda;
        }
    }
}
