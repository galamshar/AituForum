using Forum.Domain.SeedWork;

using System.Threading;
using System.Threading.Tasks;

namespace Forum.Domain.PostingAggregate
{
    public interface ITopicRepository : IRepository
    {
        Task<Topic> FindById(
            long topicId,
            CancellationToken cancellationToken = default);
        Task<Page<Topic>> FindByCreatorId(
            long creatorId,
            Pagination pagination,
            CancellationToken cancellationToken = default);
        Task<Page<Topic>> FindRootTopics(
            Pagination pagination,
            CancellationToken cancellationToken = default);
        Task<Page<Topic>> FindSubTopics(
            long parentTopicId,
            Pagination pagination,
            CancellationToken cancellationToken = default);
        
        Task<int> CountByCreatorId(
            long creatorId,
            CancellationToken cancellationToken = default);

        Task Update(
            Topic topic,
            CancellationToken cancellationToken = default);

        Task Save(
            Topic topic,
            CancellationToken cancellationToken = default);

        Task Delete(
            long topicId,
            CancellationToken cancellationToken = default);
    }
}
