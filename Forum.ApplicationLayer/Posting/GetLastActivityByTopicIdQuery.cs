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
    public class GetLastActivityByTopicIdQuery : IRequest<Topic>
    {
        public GetLastActivityByTopicIdQuery(long topicId)
        {
            TopicId = topicId;
        }

        public long TopicId { get; set; }
    }

    public class GetLastActivityByTopicIdQueryHandler : IRequestHandler<GetLastActivityByTopicIdQuery, Topic>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public GetLastActivityByTopicIdQueryHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Topic> Handle(GetLastActivityByTopicIdQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ITopicRepository>();
            var topic = await repository.FindById(request.TopicId, cancellationToken);

            var freshUpdatedDate = new DateTimeOffset(DateTime.MinValue, TimeSpan.Zero);
            Topic freshSubTopic = topic;

            if (topic.Rules.CanOwnSubtopics)
            {
                var subtopics = (await repository.FindSubTopics(topic.Id, new Pagination(1, 1000))).Items;
                foreach (var subtopic in subtopics)
                {
                    if(subtopic.UpdatedDate > freshUpdatedDate)
                    {
                        freshUpdatedDate = subtopic.UpdatedDate;
                        freshSubTopic = subtopic;
                    }
                }
            }

            return freshSubTopic;
        }
    }
}
