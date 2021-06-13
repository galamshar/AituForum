using Forum.Domain.AuthAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Auth
{
    public class DeleteAccountByIdCommand : IRequest
    {
        public DeleteAccountByIdCommand(long accountId)
        {
            AccountId = accountId;
        }

        public long AccountId { get; }
    }

    public class DeleteUserByIdCommandHandler : AsyncRequestHandler<DeleteAccountByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override Task Handle(DeleteAccountByIdCommand request, CancellationToken cancellationToken)
        {
            return _unitOfWork
                .GetRepository<IAccountRepository>()
                .Delete(request.AccountId, cancellationToken);
        }
    }
}
