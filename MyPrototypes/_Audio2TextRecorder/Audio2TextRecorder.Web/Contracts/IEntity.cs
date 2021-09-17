namespace Audio2TextRecorder.Web.Contracts
{
    /// <summary>
    /// Represent the base class for all entities.
    /// </summary>
    public interface IEntity<T>
    {
        /// <summary>
        /// Entity unique identifier.
        /// </summary>
        public T Id { get; set; }
    }
}
