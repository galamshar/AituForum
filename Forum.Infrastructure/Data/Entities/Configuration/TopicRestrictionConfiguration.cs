using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Infrastructure.Data.Entities.Configuration
{
    public class TopicRestrictionConfiguration : IEntityTypeConfiguration<TopicRestrictionEntity>
    {
        public void Configure(EntityTypeBuilder<TopicRestrictionEntity> builder)
        {
            builder.HasKey(topicRestriction => new { topicRestriction.TopicId, topicRestriction.RoleId, topicRestriction.Type });
        }
    }
}
