using Forum.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Domain.PostingAggregate
{
    public interface IPostRepository : IRepository
    {
        Task<Post> FindById(
            long postId,
            CancellationToken cancellationToken = default);

        Task<Page<Post>> FindByTopicId(
            long topicId,
            Pagination pagination,
            CancellationToken cancellationToken = default);

        Task<Page<Post>> FindByAuthorId(
            long authorId,
            Pagination pagination,
            CancellationToken cancellationToken = default);

        Task<int> CountByAuthorId(
            long authorId,
            CancellationToken cancellationToken = default);

        Task Update(
            Post post,
            CancellationToken cancellationToken = default);

        Task Save(
            Post post,
            CancellationToken cancellationToken = default);

        Task<int> CountByTopicId(
            long topicId, 
            CancellationToken cancellationToken = default);

        Task Delete(
            long postId,
            CancellationToken cancellationToken = default);

        Task<Post> GetLastPostByTopicId(
            long topicId,
            CancellationToken cancellationToken = default);
    }
}
