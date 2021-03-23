using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data.Profiles
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
