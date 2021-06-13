using System;

namespace Forum.Domain.Services
{
    /// <summary>
    /// Incapsulates logic of getting date and time.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets current date and time on the server.
        /// </summary>
        DateTimeOffset Now { get; }

        /// <summary>
        /// Gets current date and time on the server with offset 0.
        /// </summary>
        DateTimeOffset UtcNow { get; }
    }
}
