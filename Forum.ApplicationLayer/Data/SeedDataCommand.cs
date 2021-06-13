using Forum.Domain.AuthAggregate;
using Forum.Domain.Exceptions;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Data
{
    public class SeedDataCommand : IRequest
    {

    }

    public class SeedDataCommandHandler : AsyncRequestHandler<SeedDataCommand>
    {
        private readonly ITransactionalUnitOfWork _unitOfWork;
        private readonly ISeedValuesExtractor _seedValuesExtractor;

        public SeedDataCommandHandler(
            ITransactionalUnitOfWork unitOfWork,
            ISeedValuesExtractor seedValuesExtractor)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _seedValuesExtractor = seedValuesExtractor ?? throw new ArgumentNullException(nameof(seedValuesExtractor));
        }

        protected override async Task Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {
            await SeedRoles(cancellationToken);
            await SeedAccounts(cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
        }

        private async Task SeedRoles(CancellationToken cancellationToken)
        {
            var roleRepository = _unitOfWork.GetRepository<IRoleRepository>();

            foreach (var role in Role.Values)
            {
                if (!await roleRepository.ExistsByName(role.Name))
                {
                    await roleRepository.Save(role, cancellationToken);
                }
            }
        }

        private async Task SeedAccounts(CancellationToken cancellationToken = default)
        {
            var accountsToSeed = await _seedValuesExtractor.GetSeedAccounts(cancellationToken);

            var accountRepository = _unitOfWork.GetRepository<IAccountRepository>();

            foreach (var accountToSeed in accountsToSeed)
            {
                try
                {
                    await accountRepository.FindByLogin(accountToSeed.Login, cancellationToken);

                }
                catch (EntityNotFoundException)
                {
                    await accountRepository.Save(accountToSeed);
                }
            }
        }
    }
}
