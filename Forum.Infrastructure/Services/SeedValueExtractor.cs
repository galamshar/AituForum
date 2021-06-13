using Forum.ApplicationLayer.Data;
using Forum.Domain.AuthAggregate;
using Forum.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Services
{
    public class SeedValueExtractor : ISeedValuesExtractor
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public SeedValueExtractor(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }
        public Task<List<Account>> GetSeedAccounts(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new List<Account>
            {
                Account.New("admin", "password", new Role[]{ Role.Admin, Role.Student }, _dateTimeProvider)
            });
        }
    }
}
