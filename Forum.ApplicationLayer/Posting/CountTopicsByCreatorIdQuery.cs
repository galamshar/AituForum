using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class CountTopicsByCreatorIdQuery : IRequest<int>
    {
        public CountTopicsByCreatorIdQuery(long creatorId)
        {
            CreatorId = creatorId;
        }

        public long CreatorId { get; }
    }

    public class CountTopicsByCreatorIdQueryHandler : IRequestHandler<CountTopicsByCreatorIdQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountTopicsByCreatorIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(CountTopicsByCreatorIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork
                .GetRepository<ITopicRepository>()
                .CountByCreatorId(request.CreatorId, cancellationToken);
        }
    }
}
