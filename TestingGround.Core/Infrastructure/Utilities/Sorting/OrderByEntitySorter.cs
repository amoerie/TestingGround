using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using TestingGround.Core.Domain.Internal.Contracts;

namespace TestingGround.Core.Infrastructure.Utilities.Sorting
{
    /// <summary>
    ///     Defines an EntitySorter for the OrderBy clause.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity.
    /// </typeparam>
    /// <typeparam name="TKey">
    ///     The type of the key.
    /// </typeparam>
    [DebuggerDisplay("EntitySorter ( orderby {ToString()})")]
    internal class OrderByEntitySorter <TEntity, TKey> : IEntitySorter<TEntity>
    {
        /// <summary>
        ///     The _sortDirection.
        /// </summary>
        private readonly SortDirection _sortDirection;

        /// <summary>
        ///     The _key selector.
        /// </summary>
        private readonly Expression<Func<TEntity, TKey>> _keySelector;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OrderByEntitySorter{TEntity,TKey}" /> class.
        ///     Initializes a new instance of the <see cref="OrderByEntitySorter{TEntity, TKey}" /> class.
        /// </summary>
        /// <param name="keySelector">
        ///     The key selector.
        /// </param>
        /// <param name="sortDirection">
        ///     The sortDirection.
        /// </param>
        public OrderByEntitySorter(Expression<Func<TEntity, TKey>> keySelector, SortDirection sortDirection)
        {
            _keySelector = keySelector;
            _sortDirection = sortDirection;
        }

        /// <summary>
        ///     Sorts the specified collection.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TEntity}" />.
        /// </returns>
        public IOrderedQueryable<TEntity> Sort(IQueryable<TEntity> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            if (_sortDirection == SortDirection.Ascending)
            {
                return collection.OrderBy(_keySelector);
            }

            return collection.OrderByDescending(_keySelector);
        }

        /// <summary>Returns a String that represents the current object.</summary>
        /// <returns>A string representing the object.</returns>
        public override string ToString()
        {
            string sortType = _sortDirection == SortDirection.Ascending ? string.Empty : " descending";

            return _keySelector + sortType;
        }
    }
}