using Forum.Domain.AuthAggregate;
using Forum.Infrastructure.Data;
using Forum.Infrastructure.Data.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ForumContext _db;

        public RoleRepository(ForumContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public Task<bool> ExistsByName(string roleName, CancellationToken cancellationToken = default)
        {
            return _db.Roles.AnyAsync(role => role.Name == roleName, cancellationToken);
        }

        public Task Save(Role role, CancellationToken cancellationToken = default)
        {
            _db.Roles.Add(new RoleEntity { Id = role.Key, Name = role.Name });

            return _db.SaveChangesAsync(cancellationToken);
        }
    }
}
