using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using TestingGround.Core.Domain.Internal.Bases;
using TestingGround.Core.Domain.Internal.Contracts;

namespace TestingGround.Core.Infrastructure.Utilities.Loading
{
    /// <summary>
    ///     Literally does nothing.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    [DebuggerDisplay("EntityIncluder ( Nothing Included )")]
    internal class EmptyEntityLoader<TEntity>: IEntityLoader<TEntity> where TEntity : class
    {
        public TEntity FirstOrDefault(IQueryable<TEntity> entities)
        {
            return entities.FirstOrDefault();
        }

        public TEntity First(IQueryable<TEntity> entities)
        {
            return entities.First();
        }

        public TEntity SingleOrDefault(IQueryable<TEntity> entities)
        {
            return entities.SingleOrDefault();
        }

        public TEntity Single(IQueryable<TEntity> entities)
        {
            return entities.Single();
        }

        public IList<TEntity> Load(IQueryable<TEntity> entities)
        {
            return entities.ToList();
        }

        public IQueryable<IEntityLoadModel<TEntity>> Transform(IQueryable<IEntityLoadModel<TEntity>> dynamicQueryable)
        {
            return dynamicQueryable;
        }
    }
}
