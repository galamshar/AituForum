using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Domain.SeedWork
{
    public interface ITransactionalUnitOfWork : IUnitOfWork, IDisposable
    {
        Task Rollback(CancellationToken cancellationToken = default);
        Task Commit(CancellationToken cancellationToken = default);
    }
}