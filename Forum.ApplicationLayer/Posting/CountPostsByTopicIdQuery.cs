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
    public class CountPostsByTopicIdQuery : IRequest<int>
    {
        public CountPostsByTopicIdQuery(long topicId)
        {
            TopicId = topicId;
        }

        public long TopicId { get; }
    }

    public class CountPostsByTopicIdQueryHandler : IRequestHandler<CountPostsByTopicIdQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CountPostsByTopicIdQueryHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<int> Handle(CountPostsByTopicIdQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<ITopicRepository>();
            var topic = await repository.FindById(request.TopicId, cancellationToken);
            int postsCount = 0;
            if (topic.Rules.CanOwnSubtopics)
            {
                var subtopics = (await repository.FindSubTopics(topic.Id, new Pagination(1, 1000))).Items;
                foreach (var subtopic in subtopics)
                {
                    postsCount += await _mediator.Send(new CountPostsByTopicIdQuery(subtopic.Id));
                }
            }
            else
            {
                postsCount += await _unitOfWork.GetRepository<IPostRepository>().CountByTopicId(topic.Id);
            }
            return postsCount;
        }
    }
}
