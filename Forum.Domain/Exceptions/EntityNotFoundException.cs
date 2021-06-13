using System;

namespace Forum.Domain.Exceptions
{
    /// <summary>
    /// Exceptions that is thrown when entity not found in repository.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType) : base($"Entity with type {entityType} was not found")
        {
            EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        }

        public EntityNotFoundException(Type entityType, string message) : base(message)
        {
            EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        }

        public EntityNotFoundException(Type entityType, string message, Exception innerException) : base(message, innerException)
        {
            EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        }

        /// <summary>
        /// Type of searched entity.
        /// </summary>
        public Type EntityType { get; }
    }
}
