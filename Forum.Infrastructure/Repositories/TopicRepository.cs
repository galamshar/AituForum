using AutoMapper;

using Forum.Domain.Exceptions;
using Forum.Domain.PostingAggregate;
using Forum.Domain.SeedWork;
using Forum.Infrastructure.Data;
using Forum.Infrastructure.Data.Entities;
using Forum.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ForumContext _db;
        private readonly IMapper _mapper;

        public TopicRepository(
            ForumContext db,
            IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Topic> FindById(long topicId, CancellationToken cancellationToken = default)
        {
            var topic = await _db.Topics
                .AsNoTracking()
                .Include(t => t.Restrictions)
                .SingleOrDefaultAsync(t => t.Id == topicId, cancellationToken);

            if (topic is null)
            {
                throw new EntityNotFoundException(typeof(Topic));
            }

            return _mapper.Map<Topic>(topic);
        }

        public async Task<Page<Topic>> FindByCreatorId(long creatorId, Pagination pagination, CancellationToken cancellationToken = default)
        {
                var topics = await _db.Topics
                .AsNoTracking()
                .Include(t => t.Restrictions)
                .Where(topic => topic.CreatorId == creatorId)
                .ToPageAsync(pagination, cancellationToken);

            return topics.Map(topic => _mapper.Map<Topic>(topic));
        }

        public async Task<Page<Topic>> FindRootTopics(Pagination pagination, CancellationToken cancellationToken = default)
        {
            var rootTopics = await _db.Topics
                .AsNoTracking()
                .Include(t => t.Restrictions)
                .Where(t => t.ParentId == null)
                .ToPageAsync(pagination, cancellationToken);

            return rootTopics.Map(topic => _mapper.Map<Topic>(topic));
        }

        public async Task<Page<Topic>> FindSubTopics(long parentTopicId, Pagination pagination, CancellationToken cancellationToken = default)
        {
            var subTopics = await _db.Topics
                .AsNoTracking()
                .Include(t => t.Restrictions)
                .Where(t => t.ParentId == parentTopicId)
                .ToPageAsync(pagination, cancellationToken);

            return subTopics.Map(topic => _mapper.Map<Topic>(topic));
        }

        public async Task Update(Topic topic, CancellationToken cancellationToken = default)
        {
            var dbTopic = await _db.Topics
                .Include(t => t.Restrictions)
                .SingleOrDefaultAsync(t => t.Id == topic.Id, cancellationToken);

            if (dbTopic is null)
            {
                throw new EntityNotFoundException(typeof(Topic));
            }

            _db.TopicRestrictions.RemoveRange(dbTopic.Restrictions);

            if (!topic.HasRules)
            {
                dbTopic.CanOwnPosts = true;
            }
            else
            {
                var updateTopic = _mapper.Map<TopicEntity>(topic);

                dbTopic.Name = updateTopic.Name;
                dbTopic.CanOwnPosts = updateTopic.CanOwnPosts;
                dbTopic.ViewCount = updateTopic.ViewCount;
                dbTopic.LastReplyUserId = updateTopic.LastReplyUserId;
                dbTopic.UpdatedDate = updateTopic.UpdatedDate;

                _db.TopicRestrictions.AddRange(updateTopic.Restrictions);
            }

            await _db.SaveChangesAsync(cancellationToken);
        }

        public Task Save(Topic topic, CancellationToken cancellationToken = default)
        {
            _db.Topics.Add(_mapper.Map<TopicEntity>(topic));
            return _db.SaveChangesAsync(cancellationToken);
        }

        public Task Delete(long topicId, CancellationToken cancellationToken = default)
        {
            var topicToDelete = new TopicEntity { Id = topicId };

            _db.Topics.Attach(topicToDelete);
            _db.Topics.Remove(topicToDelete);

            return _db.SaveChangesAsync(cancellationToken);
        }

        public Task<int> CountByCreatorId(long creatorId, CancellationToken cancellationToken = default)
        {
            return _db.Topics.CountAsync(topic => topic.CreatorId == creatorId, cancellationToken);
        }

        public Task GetLastActivity(long topicId)
        {
            return _db.Topics.Where(t => t.ParentId == topicId && t.CanOwnPosts).OrderByDescending(t => t.UpdatedDate).FirstOrDefaultAsync();
        }
    }
}
