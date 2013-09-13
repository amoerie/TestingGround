namespace TestingGround.Core.Domain.Internal.Contracts
{
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets or sets the unique id for this object
        /// </summary>
        int Id { get; set; }
    }
}
