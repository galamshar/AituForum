using Forum.ApplicationLayer.Exceptions;
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
    public class CreateSubTopicCommand : IRequest
    {
        public CreateSubTopicCommand(
            long creatorId,
            long parentTopicId,
            string name,
            string description,
            bool canOwnPosts,
            IEnumerable<string> rolesAllowedToRead,
            IEnumerable<string> rolesAllowedToWrite)
        {
            CreatorId = creatorId;
            ParentTopicId = parentTopicId;
            Name = name;
            Description = description;
            CanOwnSubTopics = !canOwnPosts;
            RolesAllowedToRead = new HashSet<string>(rolesAllowedToRead);
            RolesAllowedToWrite = new HashSet<string>(rolesAllowedToWrite);
        }

        public long CreatorId { get; }
        public long ParentTopicId { get; }
        public string Name { get; }
        public string Description { get; }
        public bool CanOwnSubTopics { get; }
        public IReadOnlyCollection<string> RolesAllowedToRead { get; }
        public IReadOnlyCollection<string> RolesAllowedToWrite { get; }
    }

    public class CreateSubTopicCommandHandler : AsyncRequestHandler<CreateSubTopicCommand>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateSubTopicCommandHandler(
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        protected override async Task Handle(CreateSubTopicCommand request, CancellationToken cancellationToken)
        {
            if (!await _mediator.Send(new CheckIfTopicCanOwnSubTopicsQuery(request.ParentTopicId), cancellationToken))
            {
                throw new ApplicationLogicalException("Specified topic can't own subtopics");
            }

            var rules = new TopicRules(
                request.RolesAllowedToRead.Select(Role.FromName),
                request.RolesAllowedToWrite.Select(Role.FromName),
                request.CanOwnSubTopics);

            var subTopic = Topic.NewSubTopic(
                request.CreatorId,
                request.ParentTopicId,
                rules,
                request.Name,
                request.Description,
                _dateTimeProvider);

            await _unitOfWork
                .GetRepository<ITopicRepository>()
                .Save(subTopic, cancellationToken);

            var accountRepository = _unitOfWork.GetRepository<IAccountRepository>();
            var account = await accountRepository.FindById(request.CreatorId);
            account.TopicCount += 1;
            await accountRepository.Update(account, cancellationToken);
        }
    }
}
