using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Infrastructure.Data.Entities
{
    public class TopicRestrictionEntity
    {
        public long TopicId { get; set; }
        public TopicEntity Topic { get; set; }

        public long RoleId { get; set; }
        public RoleEntity Role { get; set; }

        public TopicRestrictionType Type { get; set; }
    }
}
