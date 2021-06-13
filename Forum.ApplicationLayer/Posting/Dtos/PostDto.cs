using System;

namespace Forum.ApplicationLayer.Posting.Dtos
{
    public class PostDto
    {
        public long Id { get; set; }
        public long TopicId { get; set; }
        public long AuthorId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset WrittenDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
