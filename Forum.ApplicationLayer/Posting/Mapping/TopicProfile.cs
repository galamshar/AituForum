using AutoMapper;
using Forum.ApplicationLayer.Posting.Dtos;
using Forum.Domain.PostingAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.ApplicationLayer.Posting.Mapping
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<Topic, TopicDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CreatorId, opt => opt.MapFrom(src => src.CreatorId))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.ParentId, opt => opt.MapFrom(src => src.ParentTopicId))
                .ForMember(dst => dst.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dst => dst.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dst => dst.ViewCount, opt => opt.MapFrom(src => src.ViewCount))
                .ForMember(dst => dst.LastReplyUserId, opt => opt.MapFrom(src => src.LastReplyUserId));

        }
    }
}
