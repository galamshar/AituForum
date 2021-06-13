using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.ApplicationLayer.Posting.Dtos
{
    public class TopicDto
    {
        public long Id { get; set; }
        public long CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ParentId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int ViewCount { get; set; }
        public long? LastReplyUserId { get; set; }
    }
}
