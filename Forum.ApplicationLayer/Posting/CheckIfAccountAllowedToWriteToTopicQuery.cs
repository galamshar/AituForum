using Forum.Domain.AuthAggregate;
using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class CheckIfAccountAllowedToWriteToTopicQuery : IRequest<bool>
    {
        public CheckIfAccountAllowedToWriteToTopicQuery(long writerId, long topicId)
        {
            WriterId = writerId;
            TopicId = topicId;
        }

        public long WriterId { get; }
        public long TopicId { get; }
    }

    public class CheckIfAccountAllowedToWriteToTopicQueryHandler 
        : IRequestHandler<CheckIfAccountAllowedToWriteToTopicQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckIfAccountAllowedToWriteToTopicQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> Handle(CheckIfAccountAllowedToWriteToTopicQuery request, CancellationToken cancellationToken)
        {
            var topic = await _unitOfWork
                .GetRepository<ITopicRepository>()
                .FindById(request.TopicId, cancellationToken);

            if (!topic.HasRules || !topic.Rules.IsRestrictedForWrite)
            {
                return false;
            }

            var reader = await _unitOfWork
                .GetRepository<IAccountRepository>()
                .FindById(request.WriterId, cancellationToken);

            return reader.HasAnyRole(topic.Rules.RolesAllowedToWrite);
        }
    }
}
