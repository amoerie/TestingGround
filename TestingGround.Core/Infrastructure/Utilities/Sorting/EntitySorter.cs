using System;
using System.Linq.Expressions;
using TestingGround.Core.Domain.Internal.Contracts;

namespace TestingGround.Core.Infrastructure.Utilities.Sorting
{
    /// <summary>
    ///     Enables sorting of entities.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity.
    /// </typeparam>
    public static class EntitySorter <TEntity>
    {
        /// <summary>
        ///     Returns a <see cref="IEntitySorter{TEntity}" /> instance that allows construction of
        ///     <see cref="IEntitySorter{TEntity}" /> objects though the use of LINQ syntax.
        /// </summary>
        /// <returns>
        ///     A <see cref="IEntitySorter{TEntity}" /> instance.
        /// </returns>
        public static IEntitySorter<TEntity> AsQueryable()
        {
            return new EmptyEntitySorter<TEntity>();
        }

        /// <summary>
        ///     Creates a new <see cref="IEntitySorter{TEntity}" /> that sorts a collection of
        ///     <typeparamref name="TEntity" /> objects in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key.
        /// </typeparam>
        /// <param name="keySelector">
        ///     A function to extract a key from an element.
        /// </param>
        /// <returns>
        ///     A new <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when the <paramref name="keySelector" /> null.
        /// </exception>
        public static IEntitySorter<TEntity> OrderBy <TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            return new OrderByEntitySorter<TEntity, TKey>(keySelector, SortDirection.Ascending);
        }

        /// <summary>
        ///     Creates a new <see cref="IEntitySorter{TEntity}" /> that sorts a collection of
        ///     <typeparamref name="TEntity" /> objects in descending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key.
        /// </typeparam>
        /// <param name="keySelector">
        ///     A function to extract a key from an element.
        /// </param>
        /// <returns>
        ///     A new <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when the specified <paramref name="keySelector" />
        ///     is a null reference.
        /// </exception>
        public static IEntitySorter<TEntity> OrderByDescending <TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            return new OrderByEntitySorter<TEntity, TKey>(keySelector, SortDirection.Descending);
        }

        /// <summary>
        ///     Creates a new <see cref="IEntitySorter{TEntity}" /> that sorts a collection of
        ///     <typeparamref name="TEntity" /> objects in ascending order by using the property, specified by it's
        ///     <paramref name="propertyName" />.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property or a list of chained properties, separated by a dot.
        /// </param>
        /// <returns>
        ///     A new <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Thrown when the specified <paramref name="propertyName" /> is
        ///     empty or when the specified property could not be found on the <typeparamref name="TEntity" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="propertyName" /> is null.
        /// </exception>
        public static IEntitySorter<TEntity> OrderBy(string propertyName)
        {
            var builder = new EntitySorterBuilder<TEntity>(propertyName) {SortDirection = SortDirection.Ascending};

            return builder.BuildOrderByEntitySorter();
        }

        /// <summary>
        ///     Creates a new <see cref="IEntitySorter{TEntity}" /> that sorts a collection of
        ///     <typeparamref name="TEntity" /> objects in descending order by using the property, specified by it's
        ///     <paramref name="propertyName" />.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property or a list of chained properties, separated by a dot.
        /// </param>
        /// <returns>
        ///     A new <see cref="IEntitySorter{TEntity}" />.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Thrown when the specified <paramref name="propertyName" /> is
        ///     empty or when the specified property could not be found on the <typeparamref name="TEntity" />.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="propertyName" /> is null.
        /// </exception>
        public static IEntitySorter<TEntity> OrderByDescending(string propertyName)
        {
            var builder = new EntitySorterBuilder<TEntity>(propertyName) {SortDirection = SortDirection.Descending};

            return builder.BuildOrderByEntitySorter();
        }
    }
}