using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;
using Forum.Domain.Services;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class EditPostCommand : IRequest
    {
        public EditPostCommand(long postId, string newText)
        {
            PostId = postId;
            NewText = newText;
        }

        public long PostId { get; }
        public string NewText { get; }
    }

    public class EditPostCommandHandler : AsyncRequestHandler<EditPostCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public EditPostCommandHandler(
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        protected override async Task Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            var postRepository = _unitOfWork.GetRepository<IPostRepository>();

            var postToEdit = await postRepository.FindById(request.PostId, cancellationToken);

            postToEdit.Text = request.NewText;
            postToEdit.UpdatedDate = _dateTimeProvider.UtcNow;

            await postRepository.Update(postToEdit, cancellationToken);
        }
    }
}
