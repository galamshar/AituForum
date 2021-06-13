using AutoMapper;
using Forum.ApplicationLayer.Accounting.Dtos;
using Forum.Domain.AuthAggregate;
using Forum.Domain.SeedWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Accounting
{
    public class GetAccountInfoQuery : IRequest<AccountDto>
    {
        public GetAccountInfoQuery(long accountId)
        {
            AccountId = accountId;
        }

        public long AccountId { get; set; }
    }

    public class GetAccountInfoQueryHandler : IRequestHandler<GetAccountInfoQuery, AccountDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAccountInfoQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AccountDto> Handle(GetAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.GetRepository<IAccountRepository>()
                                     .FindById(request.AccountId);
            return _mapper.Map<AccountDto>(account);
        }
    }
}
