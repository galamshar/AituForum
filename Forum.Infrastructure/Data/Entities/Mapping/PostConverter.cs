using AutoMapper;

using Forum.Domain.PostingAggregate;

namespace Forum.Infrastructure.Data.Entities.Mapping
{
    public class PostConverter : ITypeConverter<Post, PostEntity>, ITypeConverter<PostEntity, Post>
    {
        public PostEntity Convert(Post source, PostEntity destination, ResolutionContext context)
        {
            return new PostEntity
            {
                Id = source.Id,
                AuthorId = source.AuthorId,
                Text = source.Text,
                TopicId = source.TopicId,
                UpdatedDate = source.UpdatedDate,
                WrittenDate = source.WrittenDate
            };
        }

        public Post Convert(PostEntity source, Post destination, ResolutionContext context)
        {
            return new Post(source.Id, source.AuthorId, source.TopicId, source.Text, source.WrittenDate, source.UpdatedDate);
        }
    }
}
