using Audio2TextRecorder.Web.Contracts;

namespace Audio2TextRecorder.Web.Models
{
    /// <summary>
    /// Represent the base class for all entities.
    /// </summary>
    public abstract class EntityBase : IEntity<int>
    {
        /// <inheritdoc/>
        public int Id { get; set; }
    }
}
