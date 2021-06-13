using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;

using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Forum.ApplicationLayer.Posting
{
    public class CountPostsByAuthorIdQuery : IRequest<int>
    {
        public CountPostsByAuthorIdQuery(long authorId)
        {
            AuthorId = authorId;
        }

        public long AuthorId { get; }
    }

    public class CountPostsByAuthorIdQueryHandler : IRequestHandler<CountPostsByAuthorIdQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountPostsByAuthorIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(CountPostsByAuthorIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork
                .GetRepository<IPostRepository>()
                .CountByAuthorId(request.AuthorId, cancellationToken);
        }
    }
}
