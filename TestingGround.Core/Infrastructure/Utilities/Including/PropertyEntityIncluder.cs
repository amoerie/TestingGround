using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using TestingGround.Core.Domain.Internal.Contracts;
using TestingGround.Core.Infrastructure.Attributes;

namespace TestingGround.Core.Infrastructure.Utilities.Including
{
    /// <summary>
    /// Holds a property expression that can be included in a set of entities. This will cause the property to be eagerly loaded when <see cref="IQueryable{T}"/> is loaded.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    /// <typeparam name="TProperty">The type of the property</typeparam>
    [DebuggerDisplay("EntityIncluder { ToString() } ")]
    public class PropertyEntityIncluder<TEntity, TProperty>: IEntityIncluder<TEntity> where TEntity : class
    {
        private readonly IEntityIncluder<TEntity> _baseIncluder; 
        private readonly Expression<Func<TEntity, TProperty>> _propertyExpression;

        public PropertyEntityIncluder([CanBeNull] IEntityIncluder<TEntity> baseIncluder,[NotNull] Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            _propertyExpression = propertyExpression;
            _baseIncluder = baseIncluder;
        }
        
        public IQueryable<TEntity> ExecuteInclusions(IQueryable<TEntity> entities)
        {
            if (_baseIncluder == null)
            {
                return entities.Include(_propertyExpression);
            }
            return _baseIncluder.ExecuteInclusions(entities).Include(_propertyExpression);
        }

        public override string ToString()
        {
            if (_baseIncluder != null)
                return string.Format("{0}, {1}", _baseIncluder, _propertyExpression.Body);
            return string.Format("{0}", _propertyExpression.Body);
        }
    }
}
