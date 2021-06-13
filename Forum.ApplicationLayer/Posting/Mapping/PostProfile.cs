using AutoMapper;
using Forum.ApplicationLayer.Posting.Dtos;
using Forum.Domain.PostingAggregate;

namespace Forum.ApplicationLayer.Posting.Mapping
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dst => dst.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dst => dst.TopicId, opt => opt.MapFrom(src => src.TopicId))
                .ForMember(dst => dst.WrittenDate, opt => opt.MapFrom(src => src.WrittenDate))
                .ForMember(dst => dst.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate));
        }
    }
}
