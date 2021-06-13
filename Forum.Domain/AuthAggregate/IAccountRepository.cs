using Forum.Domain.SeedWork;

using System.Threading;
using System.Threading.Tasks;

namespace Forum.Domain.AuthAggregate
{
    public interface IAccountRepository : IRepository
    {
        Task<Account> FindById(long accountId, CancellationToken cancellationToken = default);
        Task<Account> FindByLogin(string login, CancellationToken cancellationToken = default);

        Task Update(Account account, CancellationToken cancellationToken = default);

        Task Save(Account account, CancellationToken cancellationToken = default);

        Task Delete(long accountId, CancellationToken cancellationToken = default);
    }
}
