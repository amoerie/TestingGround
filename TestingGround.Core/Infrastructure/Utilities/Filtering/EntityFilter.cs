using System;
using System.Linq.Expressions;
using TestingGround.Core.Domain.Internal.Contracts;

namespace TestingGround.Core.Infrastructure.Utilities.Filtering
{
    /// <summary>
    ///     Enables filtering of entities.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity.
    /// </typeparam>
    public static class EntityFilter <TEntity>
    {
        /// <summary>
        ///     Returns a <see cref="IEntityFilter{TEntity}" /> instance that allows construction of
        ///     <see cref="IEntityFilter{TEntity}" /> objects though the use of LINQ syntax.
        /// </summary>
        /// <returns>
        ///     A <see cref="IEntityFilter{TEntity}" /> instance.
        /// </returns>
        public static IEntityFilter<TEntity> AsQueryable()
        {
            return new EmptyEntityFilter<TEntity>();
        }

        /// <summary>
        ///     Returns a <see cref="IEntityFilter{TEntity}" /> that filters a sequence based on a predicate.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate.
        /// </param>
        /// <returns>
        ///     A new <see cref="IEntityFilter{TEntity}" />.
        /// </returns>
        public static IEntityFilter<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return new WhereEntityFilter<TEntity>(predicate);
        }
    }
}