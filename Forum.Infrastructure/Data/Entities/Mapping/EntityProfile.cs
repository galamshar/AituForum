using AutoMapper;
using Forum.Domain.AuthAggregate;
using Forum.Domain.PostingAggregate;

namespace Forum.Infrastructure.Data.Entities.Mapping
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Account, AccountEntity>().ConvertUsing(new AccountConverter());
            CreateMap<AccountEntity, Account>().ConvertUsing(new AccountConverter());

            CreateMap<Post, PostEntity>().ConvertUsing(new PostConverter());
            CreateMap<PostEntity, Post>().ConvertUsing(new PostConverter());

            CreateMap<Topic, TopicEntity>().ConvertUsing(new TopicConverter());
            CreateMap<TopicEntity, Topic>().ConvertUsing(new TopicConverter());
        }
    }
}
