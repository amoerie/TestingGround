using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using QueryInterceptor;
using TestingGround.Core.Domain.Internal.Bases;
using TestingGround.Core.Domain.Internal.Contracts;
using TestingGround.Core.Infrastructure.Attributes;
using TestingGround.Core.Infrastructure.Interceptors;

namespace TestingGround.Core.Infrastructure.Utilities.Loading
{
    public class EnumerableEntityLoader <TEntity, TProperty> : IEntityLoader<TEntity> where TEntity : class
        where TProperty : Entity
    {
        private readonly IEntityLoader<TEntity> _baseLoader;
        private readonly Expression<Func<TEntity, IEnumerable<TProperty>>> _propertyExpression;

        private Expression<Func<TEntity, IEnumerable<TProperty>>> UndeletedPropertiesExpression
        {
            get
            {
                return
                    (Expression<Func<TEntity, IEnumerable<TProperty>>>)
                        new CollectionPropertyVisitor<TProperty>().Visit(_propertyExpression);
            }
        }

        public EnumerableEntityLoader([CanBeNull] IEntityLoader<TEntity> baseLoader,
            [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression)
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
            var propertyExpression = UndeletedPropertiesExpression;
            IQueryable<IEntityLoadModel<TEntity>> loadModels =
                entities.Select(
                    e =>
                        new EntityLoadModel<TEntity, IEnumerable<TProperty>>
                        {
                            Entity = e,
                            NavigationProperty = propertyExpression.Invoke(e)
                        });
/*            if (_baseLoader != null)
                loadModels = _baseLoader.Transform(loadModels);*/
            return loadModels.InterceptWith<IEntityLoadModel<TEntity>>(new CollectionPropertyVisitor<TProperty>());
        }


        public IQueryable<IEntityLoadModel<TEntity>> Transform(IQueryable<IEntityLoadModel<TEntity>> dynamicQueryable)
        {
            return dynamicQueryable;
/*            var propertyExpression = UndeletedPropertiesExpression;
            return
                dynamicQueryable.Select(
                    d =>
                        new EntityLoadModel<TEntity, IEnumerable<TProperty>>
                        {
                            Entity = d.Entity,
                            NavigationProperty = propertyExpression.Invoke(d.Entity),
                            BaseLoadModel = d
                        });*/
        }
    }
}
