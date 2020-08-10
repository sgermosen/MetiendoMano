using Web.Core.Contracts;

namespace Web.Core.Models
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
