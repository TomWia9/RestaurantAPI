using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Application.Common.Mappings
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
