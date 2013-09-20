using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TestingGround.Core.Domain.Internal.Bases;
using TestingGround.Core.Domain.Internal.Contracts;

namespace TestingGround.Core.Infrastructure.Utilities.Loading
{
    /// <summary>
    /// Provides static factory methods to create includers
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public static class EntityLoader<TEntity> where TEntity : class
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
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="property">The property to be included</param>
        /// <returns>A new instance of <see cref="IEntityLoader{TEntity}"/> containing the property inclusion</returns>
        public static IEntityLoader<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            return new PropertyEntityLoader<TEntity, TProperty>(null, property);
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
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="property">The property to be included</param>
        /// <returns>A new instance of <see cref="IEntityLoader{TEntity}"/> containing the property inclusion</returns>
        public static IEntityLoader<TEntity> Include<TProperty>(Expression<Func<TEntity, IEnumerable<TProperty>>> property) where TProperty : Entity
        {
            return new EnumerableEntityLoader<TEntity, TProperty>(null, property);
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
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="property">The property to be included</param>
        /// <returns>A new instance of <see cref="IEntityLoader{TEntity}"/> containing the property inclusion</returns>
        public static IEntityLoader<TEntity> Include<TProperty>(Expression<Func<TEntity, ICollection<TProperty>>> property) where TProperty : Entity
        {
            return new CollectionEntityLoader<TEntity, TProperty>(null, property);
        }

        /// <summary>
        ///     Returns an empty <see cref="IEntityLoader{TEntity}" /> instance that allows construction of
        ///     <see cref="IEntityLoader{TEntity}" /> objects through the use of LINQ syntax.
        /// </summary>
        /// <returns>
        ///     A <see cref="IEntityLoader{TEntity}" /> instance.
        /// </returns>
        public static IEntityLoader<TEntity> AsQueryable()
        {
            return new EmptyEntityLoader<TEntity>();
        }
    }
}