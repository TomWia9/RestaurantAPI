using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDto>();
            CreateMap<DishForCreationDto, Dish>();
            CreateMap<DishForUpdateDto, Dish>();
            CreateMap<Dish, DishForUpdateDto>();
        }
    }
}
