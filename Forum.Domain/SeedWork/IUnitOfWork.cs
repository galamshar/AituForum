using System;

namespace Forum.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}
