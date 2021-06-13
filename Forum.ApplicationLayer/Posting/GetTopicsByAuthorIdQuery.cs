using AutoMapper;
using Forum.ApplicationLayer.Posting.Dtos;
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
    public class GetTopicsByAuthorIdQuery : IRequest<Page<TopicDto>>
    {
        public GetTopicsByAuthorIdQuery(long accountId, Pagination pagination)
        {
            AccountId = accountId;
            Pagination = pagination;
        }

        public GetTopicsByAuthorIdQuery(long accountId, long? readerId, Pagination pagination)
        {
            AccountId = accountId;
            ReaderId = readerId;
            Pagination = pagination;
        }

        public long AccountId { get; set; }
        public long? ReaderId { get; }
        public Pagination Pagination { get; }
    }

    public class GetTopicsByAuthorIdQueryHandler : IRequestHandler<GetTopicsByAuthorIdQuery, Page<TopicDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTopicsByAuthorIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Page<TopicDto>> Handle(GetTopicsByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork
                .GetRepository<ITopicRepository>()
                .FindByCreatorId(request.AccountId, request.Pagination, cancellationToken)
                .ContinueWith(task => task.Result.Map(_mapper.Map<TopicDto>), cancellationToken);
        }
    }
}
