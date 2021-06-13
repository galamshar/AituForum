using Forum.ApplicationLayer.Exceptions;
using Forum.Domain.AuthAggregate;
using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;
using Forum.Domain.Services;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class CreatePostCommand : IRequest
    {
        public CreatePostCommand(long topicId, long authorId, string text)
        {
            TopicId = topicId;
            AuthorId = authorId;
            Text = text;
        }

        public long TopicId { get; }
        public long AuthorId { get; }
        public string Text { get; }
    }

    public class CreatePostCommandHandler : AsyncRequestHandler<CreatePostCommand>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreatePostCommandHandler(
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            if (!await _mediator.Send(
                new CheckIfAccountAllowedToWriteToTopicQuery(request.AuthorId, request.TopicId), cancellationToken))
            {
                throw new ApplicationLogicalException("You are not allowed to write to this topic");
            }
            if(await _mediator.Send(
                new CheckIfTopicCanOwnSubTopicsQuery(request.TopicId), cancellationToken))
            {
                throw new ApplicationLogicalException("Error! Topic can`t own posts");
            }

            var post = Post.New(request.AuthorId, request.TopicId, request.Text, _dateTimeProvider);

            await _unitOfWork
                .GetRepository<IPostRepository>()
                .Save(post, cancellationToken);

            var topicRepo = _unitOfWork.GetRepository<ITopicRepository>();
            var topic = await topicRepo.FindById(request.TopicId);
            topic.UpdatedDate = _dateTimeProvider.UtcNow;
            topic.LastReplyUserId = request.AuthorId;
            await topicRepo.Update(topic,cancellationToken);

            var accountRepo = _unitOfWork.GetRepository<IAccountRepository>();
            var account = await accountRepo.FindById(request.AuthorId);
            account.PostCount += 1;
            account.Score += 1;
            await accountRepo.Update(account, cancellationToken);
        }
    }
}
