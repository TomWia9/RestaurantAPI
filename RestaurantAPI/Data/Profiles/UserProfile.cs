using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantAPI.Data.Resources;
using RestaurantAPI.Models.Auth;

namespace RestaurantAPI.Data.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserSignUpResource, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        }
    }
}
