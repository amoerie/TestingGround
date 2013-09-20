using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TestingGround.Core.Domain.Internal.Bases;
using TestingGround.Core.Domain.Internal.Contracts;

namespace TestingGround.Core.Infrastructure.Utilities.Loading
{
    public static class ExtensionsForIEntityIncluder
    {
        /// <summary>
        ///     Indicates that the <paramref name="property"/> should be immediately included when fetching the data for this query.
        ///     See http://msdn.microsoft.com/en-us/library/gg671236%28v=vs.103%29.aspx for more information.
        /// </summary>
        /// <example>
        /// <code>
        ///     //To include a single reference: 
        ///     Include(e => e.Level1Reference)
        /// 
        ///     //To include a single collection: 
        ///     Include(e => e.Level1Collection)
        /// 
        ///     //To include a reference and then a reference one level down: 
        ///     Include(e => e.Level1Reference.Level2Reference)
        /// 
        ///     //To include a reference and then a collection one level down: 
        ///     Include(e => e.Level1Reference.Level2Collection)
        /// 
        ///     //To include a collection and then a reference one level down: 
        ///     .Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))
        /// 
        ///     //To include a collection and then a collection one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))
        /// 
        ///     //To include a collection and then a reference one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))
        /// 
        ///     //To include a collection and then a collection one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))
        /// 
        ///     //To include a collection, a reference, and a reference two levels down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference.Level3Reference))
        /// 
        ///     //To include a collection, a collection, and a reference two levels down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection.Select(l2 => l2.Level3Reference)))
        /// </code>
        /// </example>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="entityLoader">The entity loader</param>
        /// <param name="property">The property to be included</param>
        /// <returns>A new instance of <see cref="IEntityLoader{TEntity}"/> containing the property inclusion</returns>
        public static IEntityLoader<TEntity> Include<TEntity, TProperty>(this IEntityLoader<TEntity> entityLoader, Expression<Func<TEntity, TProperty>> property) where TEntity : class
        {
            return new PropertyEntityLoader<TEntity, TProperty>(entityLoader, property);
        }

        /// <summary>
        ///     Indicates that the <paramref name="property"/> should be immediately included when fetching the data for this query.
        ///     See http://msdn.microsoft.com/en-us/library/gg671236%28v=vs.103%29.aspx for more information.
        /// </summary>
        /// <example>
        /// <code>
        ///     //To include a single reference: 
        ///     Include(e => e.Level1Reference)
        /// 
        ///     //To include a single collection: 
        ///     Include(e => e.Level1Collection)
        /// 
        ///     //To include a reference and then a reference one level down: 
        ///     Include(e => e.Level1Reference.Level2Reference)
        /// 
        ///     //To include a reference and then a collection one level down: 
        ///     Include(e => e.Level1Reference.Level2Collection)
        /// 
        ///     //To include a collection and then a reference one level down: 
        ///     .Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))
        /// 
        ///     //To include a collection and then a collection one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))
        /// 
        ///     //To include a collection and then a reference one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))
        /// 
        ///     //To include a collection and then a collection one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))
        /// 
        ///     //To include a collection, a reference, and a reference two levels down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference.Level3Reference))
        /// 
        ///     //To include a collection, a collection, and a reference two levels down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection.Select(l2 => l2.Level3Reference)))
        /// </code>
        /// </example>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="entityLoader">The entity loader</param>
        /// <param name="property">The property to be included</param>
        /// <returns>A new instance of <see cref="IEntityLoader{TEntity}"/> containing the property inclusion</returns>
        public static IEntityLoader<TEntity> Include<TEntity, TProperty>(this IEntityLoader<TEntity> entityLoader, Expression<Func<TEntity, IEnumerable<TProperty>>> property)
            where TProperty : Entity
            where TEntity : class
        {
            return new EnumerableEntityLoader<TEntity, TProperty>(entityLoader, property);
        }
        
        /// <summary>
        ///     Indicates that the <paramref name="property"/> should be immediately included when fetching the data for this query.
        ///     See http://msdn.microsoft.com/en-us/library/gg671236%28v=vs.103%29.aspx for more information.
        /// </summary>
        /// <example>
        /// <code>
        ///     //To include a single reference: 
        ///     Include(e => e.Level1Reference)
        /// 
        ///     //To include a single collection: 
        ///     Include(e => e.Level1Collection)
        /// 
        ///     //To include a reference and then a reference one level down: 
        ///     Include(e => e.Level1Reference.Level2Reference)
        /// 
        ///     //To include a reference and then a collection one level down: 
        ///     Include(e => e.Level1Reference.Level2Collection)
        /// 
        ///     //To include a collection and then a reference one level down: 
        ///     .Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))
        /// 
        ///     //To include a collection and then a collection one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))
        /// 
        ///     //To include a collection and then a reference one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference))
        /// 
        ///     //To include a collection and then a collection one level down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection))
        /// 
        ///     //To include a collection, a reference, and a reference two levels down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Reference.Level3Reference))
        /// 
        ///     //To include a collection, a collection, and a reference two levels down: 
        ///     Include(e => e.Level1Collection.Select(l1 => l1.Level2Collection.Select(l2 => l2.Level3Reference)))
        /// </code>
        /// </example>
        /// <typeparam name="TEntity">The type of the entity</typeparam>
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="entityLoader">The entity loader</param>
        /// <param name="property">The property to be included</param>
        /// <returns>A new instance of <see cref="IEntityLoader{TEntity}"/> containing the property inclusion</returns>
        public static IEntityLoader<TEntity> Include<TEntity, TProperty>(this IEntityLoader<TEntity> entityLoader, Expression<Func<TEntity, ICollection<TProperty>>> property)
            where TProperty : Entity
            where TEntity : class
        {
            return new CollectionEntityLoader<TEntity, TProperty>(entityLoader, property);
        }
    }
}
