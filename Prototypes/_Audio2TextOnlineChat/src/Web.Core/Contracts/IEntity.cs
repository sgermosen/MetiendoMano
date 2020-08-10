namespace Web.Core.Contracts
{
    /// <summary>
    /// Represent the base class for all entities.
    /// </summary>
    public interface IEntity<T>
    {
        /// <summary>
        /// Entity unique identifier.
        /// </summary>
        T Id { get; set; }
    }
}
