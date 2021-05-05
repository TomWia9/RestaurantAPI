using System;
using MediatR;

namespace Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommand : IRequest
    {
        public Guid RestaurantId { get; set; }
        public Guid DishId { get; set; }

        public DeleteDishCommand(Guid restaurantId, Guid dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }
    }
}
