using System;
using System.Diagnostics;
using System.Linq;
using TestingGround.Core.Domain.Internal.Contracts;

namespace TestingGround.Core.Infrastructure.Utilities.Sorting
{
    [DebuggerDisplay("EmptyEntitySorter ( Unordered )")]
    public sealed class EmptyEntitySorter<TEntity> : IEntitySorter<TEntity>
    {

        /// <summary>
        ///     Throws an <see cref="InvalidOperationException" /> when called.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     An <see cref="InvalidOperationException" /> is thrown.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown an <see cref="InvalidOperationException" />
        ///     when called.
        /// </exception>
        public IOrderedQueryable<TEntity> Sort(IQueryable<TEntity> collection)
        {
            if (collection is IOrderedQueryable<TEntity>)
                return (IOrderedQueryable<TEntity>)collection;

            const string exceptionMessage =
                "The EmptyEntitySorter can not be used for sorting, please call the "
                + "OrderBy or OrderByDescending instance methods.";

            throw new InvalidOperationException(exceptionMessage);
        }

        /// <summary>Returns string.Empty.</summary>
        /// <returns>An empty string.</returns>
        public override string ToString()
        {
            // The string representation of IEntitySorter objects is used in the Debugger.
            return string.Empty;
        }
    }
}
