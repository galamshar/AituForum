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
    public class GetPostsByTopicIdQuery : IRequest<Page<PostDto>>
    {
        public GetPostsByTopicIdQuery(long topicId, long readerId, Pagination pagination)
        {
            TopicId = topicId;
            ReaderId = readerId;
            Pagination = pagination ?? throw new ArgumentNullException(nameof(pagination));
        }

        public long TopicId { get; }
        public long ReaderId { get; }
        public Pagination Pagination { get; }
    }

    public class GetPostsByTopicIdQueryHandler : IRequestHandler<GetPostsByTopicIdQuery, Page<PostDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetPostsByTopicIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Page<PostDto>> Handle(GetPostsByTopicIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _mediator.Send(
                new CheckIfAccountAllowedToReadTopicQuery(request.TopicId, request.ReaderId), cancellationToken))
            {
                throw new ApplicationLogicalException("You are not allowed to read this topic");
            }

            return await _unitOfWork
                .GetRepository<IPostRepository>()
                .FindByTopicId(request.TopicId, request.Pagination, cancellationToken)
                .ContinueWith(task => task.Result.Map(_mapper.Map<PostDto>), cancellationToken);
        }
    }
}
