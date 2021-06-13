using AutoMapper;

using Forum.Domain.SeedWork;

using Microsoft.EntityFrameworkCore.Storage;

using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Data
{
    public class TransactionalUnitOfWork : UnitOfWork, ITransactionalUnitOfWork
    {
        private readonly IDbContextTransaction _transaction;

        public TransactionalUnitOfWork(ForumContext db, IMapper mapper) : base(db, mapper)
        {
            _transaction = db.Database.BeginTransaction();
        }

        public Task Rollback(CancellationToken cancellationToken = default)
        {
            return _transaction.RollbackAsync(cancellationToken);
        }

        public Task Commit(CancellationToken cancellationToken = default)
        {
            return _transaction.CommitAsync(cancellationToken);
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
