using System;
using Domain.Dto;
using MediatR;

namespace Application.Dishes.Queries.GetDish
{
    public class GetDishQuery : IRequest<DishDto>
    {
        public GetDishQuery(Guid restaurantId, Guid dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }

        public Guid RestaurantId { get; set; }
        public Guid DishId { get; set; }
    }
}