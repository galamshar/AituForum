using Forum.Domain.AuthAggregate;
using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class CheckIfAccountAllowedToReadTopicQuery : IRequest<bool>
    {
        public CheckIfAccountAllowedToReadTopicQuery(long topicId, long readerId)
        {
            TopicId = topicId;
            ReaderId = readerId;
        }

        public long TopicId { get; }
        public long ReaderId { get; }
    }

    public class CheckIfAccountAllowedToReadTopicQueryHandler 
        : IRequestHandler<CheckIfAccountAllowedToReadTopicQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckIfAccountAllowedToReadTopicQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> Handle(CheckIfAccountAllowedToReadTopicQuery request, CancellationToken cancellationToken)
        {
            var topic = await _unitOfWork
                .GetRepository<ITopicRepository>()
                .FindById(request.TopicId, cancellationToken);

            if (!topic.HasRules || !topic.Rules.IsRestrictedForRead)
            {
                return false;
            }

            var reader = await _unitOfWork
                .GetRepository<IAccountRepository>()
                .FindById(request.ReaderId, cancellationToken);

            return reader.HasAnyRole(topic.Rules.RolesAllowedToRead);
        }
    }
}
