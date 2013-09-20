using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestingGround.Core.Domain.Internal.Bases;
using TestingGround.Core.Infrastructure.Attributes;
using TestingGround.Core.Infrastructure.Utilities.Filtering;
using TestingGround.Core.Infrastructure.Utilities.Loading;

namespace TestingGround.Core.Domain.Internal.Contracts
{
    public interface IEntityLoader<TEntity> where TEntity: class
    {
        TEntity FirstOrDefault(IQueryable<TEntity> entities);
        TEntity First(IQueryable<TEntity> entities);
        TEntity SingleOrDefault(IQueryable<TEntity> entities);
        TEntity Single(IQueryable<TEntity> entities);
        IList<TEntity> Load(IQueryable<TEntity> entities);
        IQueryable<IEntityLoadModel<TEntity>> Transform(IQueryable<IEntityLoadModel<TEntity>> dynamicQueryable);
    }
}
