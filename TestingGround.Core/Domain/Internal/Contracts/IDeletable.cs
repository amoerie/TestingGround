﻿namespace TestingGround.Core.Domain.Internal.Contracts
{
    public interface IDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this object is deleted or not.
        /// </summary>
        bool Deleted { get; set; }
    }
}
