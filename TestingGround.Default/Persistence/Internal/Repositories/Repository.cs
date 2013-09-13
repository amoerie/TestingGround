using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using QueryInterceptor;
using TestingGround.Core.Domain.Internal.Contracts;
using TestingGround.Core.Domain.Internal.Repositories;
using TestingGround.Default.Interceptors;

namespace TestingGround.Default.Persistence.Internal.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletable, IIdentifiable
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entitySet;
        private readonly IQueryable<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entitySet = context.Set<TEntity>();
            _entities = _entitySet;
        }

        /// <summary>
        ///     Gets the DbContext
        /// </summary>
        protected virtual DbContext Context
        {
            get { return _context; }
        }

        /// <summary>
        ///     Gets the entities
        /// </summary>
        protected virtual IQueryable<TEntity> Entities(IEntityFilter<TEntity> filter = null, IEntitySorter<TEntity> sorter = null, IEntityIncluder<TEntity> includer = null)
        {
            var entities = _entities;
            if (includer != null)
                entities = includer.ExecuteInclusions(entities);
            entities = entities/*.AsExpandable()*/.Where(e => !e.Deleted);
            if (filter != null)
                entities = filter.Filter(entities);
            if (sorter != null)
                entities = sorter.Sort(entities);
            entities = entities.InterceptWith(new DeletedFilterInterceptor());
            return entities;
        }

        /// <summary>
        ///     Gets the editable dbset
        /// </summary>
        protected virtual DbSet<TEntity> EntitySet
        {
            get { return _entitySet; }
        }

        public IQueryable<TEntity> List(IEntityFilter<TEntity> filter = null,
            IEntitySorter<TEntity> sorter = null,
            int? page = null,
            int? pageSize = null,
            IEntityIncluder<TEntity> includer = null)
        {
            if ((page.HasValue || pageSize.HasValue) && sorter == null)
            {
                throw new ArgumentException("You have to define a sorting order if you specify a page or pageSize! (IEntitySorter was null)");
            }

            if (page.HasValue && !pageSize.HasValue)
            {
                throw new ArgumentException("You have to define a pageSize if you specify a page!");
            }

            var entities = Entities(filter, sorter, includer);

            if (page != null)
                entities = entities.Skip(pageSize.Value * page.Value);

            if (pageSize != null)
                entities = entities.Take(pageSize.Value);

            return entities;
        }

        public virtual int Count(IEntityFilter<TEntity> filter = null)
        {
            return Entities(filter).Count();
        }

        public bool Any(IEntityFilter<TEntity> filter = null)
        {
            return Entities(filter).Any();
        }

        public TEntity SingleOrDefault(IEntityFilter<TEntity> filter = null, IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, includer: includer).SingleOrDefault();
        }

        public TEntity Single(IEntityFilter<TEntity> filter = null, IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, includer: includer).Single();
        }

        public TEntity FirstOrDefault(IEntityFilter<TEntity> filter = null, IEntitySorter<TEntity> sorter = null, IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, sorter, includer).FirstOrDefault();
        }

        public TEntity First(IEntityFilter<TEntity> filter = null, IEntitySorter<TEntity> sorter = null, IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, sorter, includer).First();
        }

        public IEnumerable<TResult> Select<TResult>(Func<TEntity, TResult> selector,
            IEntityFilter<TEntity> filter = null,
            IEntitySorter<TEntity> sorter = null,
            IEntityIncluder<TEntity> includer = null)
        {
            return Entities(filter, sorter, includer).Select(selector);
        }

        public virtual TEntity Find(int id)
        {
            return EntitySet.Find(id);
        }

        public virtual void AddOrUpdate(TEntity entity)
        {
            if (entity.Id == 0)
            {
                Add(entity);
            }
            else
            {
                Update(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Delete(entity);
            }
        }

        public virtual void Delete(int id)
        {
            TEntity entity = Find(id);
            if (entity != null)
                Delete(entity);
        }

        public virtual void HardDelete(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                EntitySet.Attach(entity);
                EntitySet.Remove(entity);
            }
        }

        public virtual void HardDelete(int id)
        {
            TEntity entity = Find(id);
            if (entity != null)
                HardDelete(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public TResult Query<TResult>(Expression<Func<IQueryable<TEntity>, TResult>> query)
        {
            //var filteredDeleted = (Expression<Func<IQueryable<TEntity>, TResult>>) new DeletedFilterInterceptor().Visit(query);
            return query.Compile()(Entities());
        }

        protected virtual void Add(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                EntitySet.Add(entity);
            }
        }

        protected virtual void Update(TEntity entity)
        {
            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                EntitySet.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }
    }
}
