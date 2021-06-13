using Forum.Domain.SeedWork;

using System.Threading;
using System.Threading.Tasks;

namespace Forum.Domain.AuthAggregate
{
    public interface IRoleRepository : IRepository
    {
        Task<bool> ExistsByName(string roleName, CancellationToken cancellationToken = default);

        Task Save(Role role, CancellationToken cancellationToken = default);
    }
}
