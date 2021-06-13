using Forum.Domain.AuthAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Auth
{
    public class SignInCommand : IRequest
    {
        public SignInCommand(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }
        public string Password { get; }
    }

    public class SignInCommandHandler : AsyncRequestHandler<SignInCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthContext _authContext;
        private readonly IPasswordHasher _passwordHasher;

        public SignInCommandHandler(
            IUnitOfWork unitOfWork,
            IAuthContext authContext,
            IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        protected override async Task Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var accountRepo = _unitOfWork.GetRepository<IAccountRepository>();
            var account = await accountRepo
                .FindByLogin(request.Login, cancellationToken);
            await accountRepo.Update(account);

            if (_passwordHasher.Verify(account.Password, request.Password))
            {
                await _authContext.SignIn(account, cancellationToken);
            }
        }
    }
}
