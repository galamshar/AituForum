using Forum.Domain.AuthAggregate;
using Forum.Domain.Services;

using System;

namespace Forum.Domain.PostingAggregate
{
    public class Topic
    {
        // !!!. This is for auto-mapper. No direct use.
        private Topic() { }

        public Topic(
            long id,
            long? creatorId,
            long? parentTopicId,
            TopicRules rules,
            string name,
            string description,
            DateTimeOffset createdDate,
            DateTimeOffset updatedDate,
            int viewCount,
            long? lastReplyUserId
            )
        {
            Id = id;
            CreatorId = creatorId;
            Name = name;
            Description = description;
            ParentTopicId = parentTopicId;
            Rules = rules;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            ViewCount = viewCount;
            LastReplyUserId = lastReplyUserId;
        }

        public long Id { get; set; }

        public bool HasCreator => CreatorId.HasValue;
        public long? CreatorId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public long? ParentTopicId { get; set; }

        public bool IsRootTopic => !IsSubTopic;
        public bool IsSubTopic => ParentTopicId.HasValue;

        public bool HasRules => Rules is { };
        public TopicRules Rules { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int ViewCount { get; set; }
        public long? LastReplyUserId { get; set; }

        public static Topic NewRootTopic(
            long creatorId,
            TopicRules rules,
            string name,
            string description,
            IDateTimeProvider dateTimeProvider)
        {
            var now = dateTimeProvider.UtcNow;

            return new Topic(0L, creatorId, null, rules, name, description, now, now, 0, null);
        }

        public static Topic NewSubTopic(
            long creatorId,
            long parentTopicId,
            TopicRules rules,
            string name,
            string description,
            IDateTimeProvider dateTimeProvider)
        {
            var now = dateTimeProvider.UtcNow;

            return new Topic(0L, creatorId, parentTopicId, rules, name, description, now, now, 0, null);
        }
    }
}
