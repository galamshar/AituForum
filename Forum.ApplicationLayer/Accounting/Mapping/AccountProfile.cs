using AutoMapper;
using Forum.ApplicationLayer.Accounting.Dtos;
using Forum.Domain.AuthAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.ApplicationLayer.Accounting.Mapping
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dst => dst.TopicCount, opt => opt.MapFrom(src => src.TopicCount))
                .ForMember(dst => dst.PostCount, opt => opt.MapFrom(src => src.PostCount))
                .ForMember(dst => dst.Score, opt => opt.MapFrom(src => src.Score))
                .ForMember(dst => dst.CreateOn, opt => opt.MapFrom(src => src.CreateOn))
                .ForMember(dst => dst.LastTime, opt => opt.MapFrom(src => src.LastTime))
                .ForMember(dst => dst.Roles, opt => opt.MapFrom(src => src.Roles));
        }
    }
}
