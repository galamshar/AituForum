using Forum.Domain.AuthAggregate;

using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Auth
{
    /// <summary>
    /// Defines mechanism of authenticating account in application.
    /// </summary>
    public interface IAuthContext
    {
        Task<Account> GetSignedAccount(CancellationToken cancellationToken = default);

        Task SignIn(Account account, CancellationToken cancellationToken = default);
        Task Update(Account account, CancellationToken cancellationToken = default);

        Task SignOut(CancellationToken cancellationToken = default);
    }
}
