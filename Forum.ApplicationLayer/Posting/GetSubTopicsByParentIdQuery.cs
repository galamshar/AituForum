using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class GetSubTopicsByParentIdQuery : IRequest<Page<Topic>>
    {
        public GetSubTopicsByParentIdQuery(long parentId, Pagination pagination)
        {
            ParentId = parentId;
            Pagination = pagination;
        }

        public long ParentId { get; }
        public Pagination Pagination { get; }
    }

    public class GetSubTopicsByRootTopicIdQueryHandler : IRequestHandler<GetSubTopicsByParentIdQuery, Page<Topic>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSubTopicsByRootTopicIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task<Page<Topic>> Handle(GetSubTopicsByParentIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork
                .GetRepository<ITopicRepository>()
                .FindSubTopics(request.ParentId, request.Pagination, cancellationToken);
        }
    }
}
