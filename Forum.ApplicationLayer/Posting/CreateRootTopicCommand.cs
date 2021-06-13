using Forum.Domain.AuthAggregate;
using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;
using Forum.Domain.Services;

using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class CreateRootTopicCommand : IRequest
    {
        public CreateRootTopicCommand(
            long creatorId,
            string name,
            string description,
            bool canOwnPosts,
            IEnumerable<string> rolesAllowedToRead,
            IEnumerable<string> rolesAllowedToWrite)
        {
            CreatorId = creatorId;
            Name = name;
            Description = description;
            CanOwnSubTopics = !canOwnPosts;
            RolesAllowedToRead = new HashSet<string>(rolesAllowedToRead);
            RolesAllowedToWrite = new HashSet<string>(rolesAllowedToWrite);
        }

        public long CreatorId { get; }
        public string Name { get; }
        public string Description { get; }
        public bool CanOwnSubTopics { get; }
        public IReadOnlyCollection<string> RolesAllowedToRead { get; }
        public IReadOnlyCollection<string> RolesAllowedToWrite { get; }
    }

    public class CreateRootTopicCommandHandler : AsyncRequestHandler<CreateRootTopicCommand>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRootTopicCommandHandler(
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _dateTimeProvider = dateTimeProvider ??
                throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        protected override async Task Handle(CreateRootTopicCommand request, CancellationToken cancellationToken)
        {
            var rules = new TopicRules(
                request.RolesAllowedToRead.Select(Role.FromName),
                request.RolesAllowedToWrite.Select(Role.FromName),
                request.CanOwnSubTopics);

            var topic = Topic.NewRootTopic(request.CreatorId, rules, request.Name, request.Description, _dateTimeProvider);

            await _unitOfWork
                .GetRepository<ITopicRepository>()
                .Save(topic, cancellationToken);
        }
    }
}