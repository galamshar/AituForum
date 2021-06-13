using Forum.Domain.AuthAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Auth
{
    public class ChangePasswordCommand : IRequest
    {
        public long AccountId { get; }
        public string Password { get; }
    }

    public class ChangePasswordCommandHandler : AsyncRequestHandler<ChangePasswordCommand>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthContext _authContext;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(
            IPasswordHasher passwordHasher,
            IAuthContext authContext,
            IUnitOfWork unitOfWork)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var accountRepository = _unitOfWork.GetRepository<IAccountRepository>();

            var account = await accountRepository
                .FindById(request.AccountId, cancellationToken);

            account.Password = _passwordHasher.Hash(request.Password);

            await accountRepository.Update(account, cancellationToken);

            await _authContext.Update(account, cancellationToken);
        }
    }
}
