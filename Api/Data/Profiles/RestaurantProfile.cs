using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<AddressDto, Address>()
                .ForMember(a => a.Id, opt => opt.Ignore());
            CreateMap<Address, AddressDto>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantForCreationDto, Restaurant>();
            CreateMap<RestaurantForUpdateDto, Restaurant>();
            CreateMap<Restaurant, RestaurantForUpdateDto>();
        }
    }
}
