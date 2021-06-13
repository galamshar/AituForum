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
    public class TopicCheckedQuery : IRequest
    {
        public TopicCheckedQuery(long topicId)
        {
            TopicId = topicId;
        }

        public long TopicId { get;}
    }

    public class TopicCheckedQueryHandler : AsyncRequestHandler<TopicCheckedQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TopicCheckedQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(TopicCheckedQuery request, CancellationToken cancellationToken)
        {
            var topicRepository = _unitOfWork.GetRepository<ITopicRepository>();
            var topic = await topicRepository.FindById(request.TopicId);
            topic.ViewCount += 1;
            await topicRepository.Update(topic);
        }
    }
}
