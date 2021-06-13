using Forum.Domain.Services;

using System;

namespace Forum.Infrastructure.Services
{
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;

        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
