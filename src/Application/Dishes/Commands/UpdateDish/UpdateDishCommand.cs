using System;
using Domain.Dto;
using MediatR;

namespace Application.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommand : IRequest
    {
        public UpdateDishCommand(Guid restaurantId, Guid dishId, DishForUpdateDto dishForUpdate)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
            DishForUpdate = dishForUpdate;
        }

        public Guid RestaurantId { get; set; }
        public Guid DishId { get; set; }
        public DishForUpdateDto DishForUpdate { get; set; }
    }
}