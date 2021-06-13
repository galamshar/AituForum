using AutoMapper;

using Forum.ApplicationLayer.Exceptions;
using Forum.ApplicationLayer.Posting.Dtos;
using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class GetPostByIdQuery : IRequest<PostDto>
    {
        public GetPostByIdQuery(long readerId, long postId)
        {
            ReaderId = readerId;
            PostId = postId;
        }

        public long ReaderId { get; }
        public long PostId { get; }
    }

    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork
                .GetRepository<IPostRepository>()
                .FindById(request.PostId, cancellationToken);

            if (!await _mediator.Send(new CheckIfAccountAllowedToReadTopicQuery(post.TopicId, request.ReaderId)))
            {
                throw new ApplicationLogicalException("You are not allowed to read post of this topic");
            }

            return _mapper.Map<PostDto>(post);
        }
    }
}
