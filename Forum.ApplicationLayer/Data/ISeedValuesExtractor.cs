using Forum.Domain.AuthAggregate;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Data
{
    public interface ISeedValuesExtractor
    {
        Task<List<Account>> GetSeedAccounts(CancellationToken cancellationToken = default);
    }
}
