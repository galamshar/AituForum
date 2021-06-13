using AutoMapper;

using Forum.ApplicationLayer.Posting.Dtos;
using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class GetPostsByAuthorIdQuery : IRequest<Page<PostDto>>
    {
        public GetPostsByAuthorIdQuery(long authorId, Pagination pagination)
        {
            AuthorId = authorId;
            Pagination = pagination;
        }

        public GetPostsByAuthorIdQuery(long authorId, long readerId, Pagination pagination)
        {
            AuthorId = authorId;
            ReaderId = readerId;
            Pagination = pagination;
        }

        public long AuthorId { get; }
        public long? ReaderId { get; }
        public Pagination Pagination { get; }
    }

    public class GetPostsByAuthorIdQueryHandler : IRequestHandler<GetPostsByAuthorIdQuery, Page<PostDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPostsByAuthorIdQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<Page<PostDto>> Handle(GetPostsByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork
                .GetRepository<IPostRepository>()
                .FindByAuthorId(request.AuthorId, request.Pagination, cancellationToken)
                .ContinueWith(task => task.Result.Map(_mapper.Map<PostDto>), cancellationToken);
        }
    }
}
