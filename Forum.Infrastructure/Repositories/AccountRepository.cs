using Forum.Domain.AuthAggregate;
using Forum.Domain.Exceptions;
using Forum.Infrastructure.Data;
using Forum.Infrastructure.Data.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ForumContext _db;

        public AccountRepository(ForumContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Account> FindById(long accountId, CancellationToken cancellationToken = default)
        {
            var account = await _db.Accounts
                .AsNoTracking()
                .Include(a => a.Roles)
                .ThenInclude(ar => ar.Role)
                .SingleOrDefaultAsync(a => a.Id == accountId, cancellationToken);

            if (account is null)
            {
                throw new EntityNotFoundException(typeof(Account));
            }

            var roles = account.Roles
                .Select(ar => Role.FromName(ar.Role.Name))
                .ToList();

            return new Account(account.Id, account.Login, account.Password, account.TopicCount, account.PostCount, account.Score, account.CreateOn, account.LastTime, roles);
        }

        public async Task<Account> FindByLogin(string login, CancellationToken cancellationToken = default)
        {
            var account = await _db.Accounts
                .AsNoTracking()
                .Include(a => a.Roles)
                .ThenInclude(ar => ar.Role)
                .SingleOrDefaultAsync(a => a.Login == login, cancellationToken);

            if (account is null)
            {
                throw new EntityNotFoundException(typeof(Account));
            }

            var roles = account.Roles
                .Select(ar => Role.FromName(ar.Role.Name))
                .ToList();

            return new Account(account.Id, account.Login, account.Password, account.TopicCount, account.PostCount, account.Score, account.CreateOn, account.LastTime, roles);
        }

        public async Task Save(Account account, CancellationToken cancellationToken = default)
        {
            var accountEntity = new AccountEntity
            {
                Login = account.Login,
                Password = account.Password,
                Roles = account.Roles.Select(r => new AccountRoleEntity { RoleId = r.Key }).ToList(),
                CreateOn = account.CreateOn,
                LastTime = account.LastTime
            };

            _db.Accounts.Add(accountEntity);

            await _db.SaveChangesAsync(cancellationToken);

            account.Id = accountEntity.Id;
        }

        public async Task Update(Account account, CancellationToken cancellationToken = default)
        {
            var accountEntity = await _db.Accounts
                .SingleOrDefaultAsync(a => a.Id == account.Id, cancellationToken);

            accountEntity.Login = account.Login;
            accountEntity.Password = account.Password;
            accountEntity.TopicCount = account.TopicCount;
            accountEntity.PostCount = account.PostCount;
            accountEntity.Score = account.Score;
            accountEntity.LastTime = DateTimeOffset.UtcNow;
            /*accountEntity.Roles = account.Roles
                .Select(r => new AccountRoleEntity { AccountId = accountEntity.Id, RoleId = r.Key })
                .ToList();*/

            await _db.SaveChangesAsync(cancellationToken);
        }

        public Task Delete(long accountId, CancellationToken cancellationToken = default)
        {
            // Hack to delete entities without querying database
            var accountToDelete = new AccountEntity { Id = accountId };

            _db.Accounts.Attach(accountToDelete);
            _db.Accounts.Remove(accountToDelete);

            return _db.SaveChangesAsync(cancellationToken);
        }
    }
}
