using System;
using TestingGround.Core.Domain.Internal.Contracts;

namespace TestingGround.Core.Domain.Internal.Bases
{
    /// <summary>
    ///     This is the base class for all entities (classes that need to be persisted to the database)
    /// </summary>
    public abstract class Entity: IIdentifiable, IDeletable
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public virtual int Id { get; set; }
        
        /// <summary>
        ///     Gets or sets a value indicating whether deleted.
        /// </summary>
        public virtual bool Deleted { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Id: {0}", Id);
        }
    }
}