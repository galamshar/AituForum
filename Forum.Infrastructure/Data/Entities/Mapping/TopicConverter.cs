using AutoMapper;

using Forum.Domain.AuthAggregate;
using Forum.Domain.PostingAggregate;

using System.Collections.Generic;
using System.Linq;

namespace Forum.Infrastructure.Data.Entities.Mapping
{
    public class TopicConverter : ITypeConverter<Topic, TopicEntity>, ITypeConverter<TopicEntity, Topic>
    {
        public Topic Convert(TopicEntity source, Topic destination, ResolutionContext context)
        {
            var readRestrictions = source.Restrictions
                .Where(r => r.Type == TopicRestrictionType.Read)
                .Select(r => Role.FromKey(r.RoleId))
                .ToList();

            var writeRestrictions = source.Restrictions
                .Where(r => r.Type == TopicRestrictionType.Write)
                .Select(r => Role.FromKey(r.RoleId))
                .ToList();

            TopicRules rules = null;

            if (readRestrictions.Any() || writeRestrictions.Any() || !source.CanOwnPosts)
            {
                rules = new TopicRules(readRestrictions, writeRestrictions, !source.CanOwnPosts);
            }
            return new Topic(source.Id, source.CreatorId, source.ParentId, rules, source.Name,source.Description, source.CreatedDate, source.UpdatedDate, source.ViewCount, source.LastReplyUserId);
        }

        public TopicEntity Convert(Topic source, TopicEntity destination, ResolutionContext context)
        {
            var restrictions = new List<TopicRestrictionEntity>();

            if (source.HasRules)
            {
                restrictions.AddRange(source.Rules.RolesAllowedToRead
                    .Select(r => new TopicRestrictionEntity { RoleId = r.Key, TopicId = source.Id, Type = TopicRestrictionType.Read } ));

                restrictions.AddRange(source.Rules.RolesAllowedToWrite
                    .Select(r => new TopicRestrictionEntity { RoleId = r.Key, TopicId = source.Id, Type = TopicRestrictionType.Write }));
            }

            return new TopicEntity
            {
                Id = source.Id,
                CanOwnPosts = !source.HasRules || source.Rules.CanOwnPosts,
                Name = source.Name,
                Description = source.Description,
                ParentId = source.ParentTopicId,
                UpdatedDate = source.UpdatedDate,
                CreatedDate = source.CreatedDate,
                CreatorId = source.CreatorId,
                Restrictions = restrictions,
                LastReplyUserId = source.LastReplyUserId
                
            };
        }
    }
}
