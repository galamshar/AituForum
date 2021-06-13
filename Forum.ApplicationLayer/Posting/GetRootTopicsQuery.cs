using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class GetRootTopicsQuery : IRequest<Page<Topic>>
    {
        public GetRootTopicsQuery(Pagination pagination)
        {
            Pagination = pagination;
        }

        public Pagination Pagination { get; }
    }

    public class GetRootTopicsQueryHandler : IRequestHandler<GetRootTopicsQuery, Page<Topic>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRootTopicsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task<Page<Topic>> Handle(GetRootTopicsQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork
                .GetRepository<ITopicRepository>()
                .FindRootTopics(request.Pagination, cancellationToken);
        }
    }
}
