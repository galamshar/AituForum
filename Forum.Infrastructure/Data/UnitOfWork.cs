using AutoMapper;

using Forum.Domain.SeedWork;

using System;
using System.Linq;

namespace Forum.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumContext _db;
        private readonly IMapper _mapper;

        public UnitOfWork(ForumContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            var repositoryType = GetType()
                .Assembly
                .GetTypes()
                .Where(type => typeof(TRepository).IsAssignableFrom(type))
                .FirstOrDefault();

            var ctors = repositoryType
                .GetConstructors();

            foreach (var ctor in ctors)
            {
                var parameters = ctor.GetParameters();

                if (parameters.Length == 1)
                {
                    return (TRepository)ctor.Invoke(new object[] { _db });
                }
                else if (parameters.Length == 2)
                {
                    if (parameters[0].ParameterType == typeof(IMapper))
                    {
                        return (TRepository)ctor.Invoke(new object[] { _mapper, _db });
                    }

                    return (TRepository)ctor.Invoke(new object[] { _db, _mapper });
                }
            }

            throw new InvalidOperationException();
        }
    }
}
