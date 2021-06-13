using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Infrastructure.Data.Entities
{
    public class PostEntity
    {
        public long Id { get; set; }

        public long AuthorId { get; set; }
        public AccountEntity Author { get; set; }

        public long TopicId { get; set; }
        public TopicEntity Topic { get; set; }

        public string Text { get; set; }

        public DateTimeOffset WrittenDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}