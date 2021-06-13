using Forum.Domain.AuthAggregate;
using Forum.Domain.SeedWork;
using Forum.Domain.Services;
using MediatR;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Auth
{
    public class RegisterAccountCommand : IRequest
    {
        public RegisterAccountCommand(
            string login,
            string password,
            IEnumerable<Role> roles,
            bool signIn)
        {
            Login = login;
            Password = password;
            Roles = new HashSet<Role>(roles);
            SignIn = signIn;
        }

        public string Login { get; }
        public string Password { get; }
        public ISet<Role> Roles { get; }
        public bool SignIn { get; }
    }

    public class RegisterAccountCommandHandler : AsyncRequestHandler<RegisterAccountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthContext _authContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IDateTimeProvider _dateTimeProvider;

        public RegisterAccountCommandHandler(
            IUnitOfWork unitOfWork,
            IAuthContext authContext,
            IPasswordHasher passwordHasher,
            IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _authContext = authContext ?? throw new ArgumentNullException(nameof(authContext));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        protected override async Task Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            var account = Account.New(request.Login, _passwordHasher.Hash(request.Password), request.Roles, _dateTimeProvider);

            await _unitOfWork
                .GetRepository<IAccountRepository>()
                .Save(account, cancellationToken);

            if (request.SignIn)
            {
                await _authContext.SignIn(account, cancellationToken);
            }
        }
    }
}
