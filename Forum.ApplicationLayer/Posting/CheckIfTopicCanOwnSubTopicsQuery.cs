using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class CheckIfTopicCanOwnSubTopicsQuery : IRequest<bool>
    {
        public CheckIfTopicCanOwnSubTopicsQuery(long topicId)
        {
            TopicId = topicId;
        }

        public long TopicId { get; }
    }

    public class CheckIfTopicCanOwnSubTopicsQueryHandler : IRequestHandler<CheckIfTopicCanOwnSubTopicsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckIfTopicCanOwnSubTopicsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> Handle(CheckIfTopicCanOwnSubTopicsQuery request, CancellationToken cancellationToken)
        {
            var topic = await _unitOfWork
                .GetRepository<ITopicRepository>()
                .FindById(request.TopicId, cancellationToken);

            return !topic.HasRules || topic.Rules.CanOwnSubtopics;
        }
    }
}
