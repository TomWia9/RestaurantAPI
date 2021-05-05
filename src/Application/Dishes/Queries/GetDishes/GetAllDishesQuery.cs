using System;
using Application.Common.Models;
using Domain.Dto;
using Domain.ResourceParameters;
using MediatR;

namespace Application.Dishes.Queries.GetDishes
{
    public class GetAllDishesQuery : IRequest<PagedList<DishDto>>
    {
        public Guid RestaurantId { get; set; }
        public DishesResourceParameters DishesResourceParameters { get; set; }

        public GetAllDishesQuery(Guid restaurantId, DishesResourceParameters dishesResourceParameters)
        {
            RestaurantId = restaurantId;
            DishesResourceParameters = dishesResourceParameters;
        }

    }
}
