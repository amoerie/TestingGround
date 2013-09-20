using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using TestingGround.Core.Domain.Internal.Contracts;
using TestingGround.Core.Infrastructure.Attributes;

namespace TestingGround.Core.Infrastructure.Utilities.Loading
{
    /// <summary>
    /// Holds a property expression that can be included in a set of entities. This will cause the property to be eagerly loaded when <see cref="IQueryable{T}"/> is loaded.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    /// <typeparam name="TProperty">The type of the property</typeparam>
    [DebuggerDisplay("EntityIncluder { ToString() } ")]
    public class PropertyEntityLoader<TEntity, TProperty>: IEntityLoader<TEntity> where TEntity : class
    {
        private readonly IEntityLoader<TEntity> _baseLoader; 
        private readonly Expression<Func<TEntity, TProperty>> _propertyExpression;

        public PropertyEntityLoader([CanBeNull] IEntityLoader<TEntity> baseLoader,[NotNull] Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            _propertyExpression = propertyExpression;
            _baseLoader = baseLoader;
        }

        public override string ToString()
        {
            if (_baseLoader != null)
                return string.Format("{0}, {1}", _baseLoader, _propertyExpression.Body);
            return string.Format("{0}", _propertyExpression.Body);
        }

        public TEntity FirstOrDefault(IQueryable<TEntity> entities)
        {
            var firstOrDefault = GetLoadModels(entities).FirstOrDefault();
            return firstOrDefault != null ? firstOrDefault.Entity : null;
        }

        public TEntity First(IQueryable<TEntity> entities)
        {
            var first = GetLoadModels(entities).First();
            return first.Entity;
        }

        public TEntity SingleOrDefault(IQueryable<TEntity> entities)
        {
            var singleOrDefault = GetLoadModels(entities).SingleOrDefault();
            return singleOrDefault != null ? singleOrDefault.Entity : null;
        }

        public TEntity Single(IQueryable<TEntity> entities)
        {
            var single = GetLoadModels(entities).Single();
            return single.Entity;
        }

        public IList<TEntity> Load(IQueryable<TEntity> entities)
        {
            return GetLoadModels(entities).ToList().Select(a => a.Entity).ToList();
        }

        private IQueryable<IEntityLoadModel<TEntity>> GetLoadModels(IQueryable<TEntity> entities)
        {
            var propertyExpression = _propertyExpression;
            IQueryable<IEntityLoadModel<TEntity>> loadModels =
                entities.Select(
                    e =>
                        new EntityLoadModel<TEntity, TProperty>
                        {
                            Entity = e,
                            NavigationProperty = propertyExpression.Invoke(e)
                        });
            /*            if (_baseLoader != null)
                            loadModels = _baseLoader.Transform(loadModels);*/
            return loadModels;
        }


        public IQueryable<IEntityLoadModel<TEntity>> Transform(IQueryable<IEntityLoadModel<TEntity>> dynamicQueryable)
        {
            var propertyExpression = _propertyExpression;
            return
                dynamicQueryable.Select(
                    d =>
                        new EntityLoadModel<TEntity, TProperty>
                        {
                            Entity = d.Entity,
                            NavigationProperty = propertyExpression.Invoke(d.Entity),
                            BaseLoadModel = d
                        });
        }
    }
}
