/*using AutoMapper;

using Forum.ApplicationLayer.Exceptions;
using Forum.ApplicationLayer.Posting.Dtos;
using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class GetAllPostsByTopicIdQuery : IRequest<List<Post>>
    {
        public GetAllPostsByTopicIdQuery(long topicId, long readerId)
        {
            TopicId = topicId;
            ReaderId = readerId;
        }

        public long TopicId { get; }
        public long ReaderId { get; }

    }

    public class GetAllPostsByTopicIdQueryHandler : IRequestHandler<GetAllPostsByTopicIdQuery, List<PostDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GetAllPostsByTopicIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<PostDto>> Handle(GetAllPostsByTopicIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _mediator.Send(
                new CheckIfAccountAllowedToReadTopicQuery(request.TopicId, request.ReaderId), cancellationToken))
            {
                throw new ApplicationLogicalException("You are not allowed to read this topic");
            }

            return await _unitOfWork
                .GetRepository<IPostRepository>()
                .FindAllByTopicId(request.TopicId, cancellationToken)
                .ContinueWith(task => _mapper.Map<List<PostDto>>(task.Result), cancellationToken);
        }
    }
}
*/