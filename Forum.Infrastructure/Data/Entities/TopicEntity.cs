using Forum.Domain.AuthAggregate;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Infrastructure.Data.Entities
{
    public class TopicEntity
    {
        public long Id { get; set; }

        public long? CreatorId { get; set; }
        public AccountEntity Creator { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public long? ParentId { get; set; }
        public TopicEntity Parent { get; set; }

        public bool CanOwnPosts { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int ViewCount { get; set; }
        public long? LastReplyUserId { get; set; }
        public AccountEntity LastReplyUser { get; set; }

        public ICollection<TopicRestrictionEntity> Restrictions { get; set; }
    }
}