using Forum.Domain.Services;

using System;

namespace Forum.Domain.PostingAggregate
{
    public class Post
    {
        private DateTimeOffset _updatedDate;
        private string _text;

        // !!!. This is for auto-mapper. No direct use.
        private Post() { }

        public Post(
            long id,
            long authorId,
            long topicId,
            string text,
            DateTimeOffset writtenDate,
            DateTimeOffset updatedDate)
        {
            Id = id;
            AuthorId = authorId;
            Text = text;
            TopicId = topicId;
            WrittenDate = writtenDate;
            UpdatedDate = updatedDate;
        }

        public long Id { get; set; }

        public long AuthorId { get; set; }

        public long TopicId { get; set; }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Comment's text can't be empty", nameof(Text));
                }

                _text = value;
            }
        }

        public DateTimeOffset WrittenDate { get; set; }

        public DateTimeOffset UpdatedDate
        {
            get
            {
                return _updatedDate;
            }
            set
            {
                if (value < WrittenDate)
                {
                    throw new ArgumentOutOfRangeException(nameof(WrittenDate), value, 
                        "Update date can't be earlier than written date");
                }

                _updatedDate = value;
            }
        }

        public static Post New(long authorId, long topicId, string text, IDateTimeProvider dateTimeProvider)
        {
            var now = dateTimeProvider.UtcNow;
            return new Post(0L, authorId, topicId, text, now, now);
        }
    }
}
