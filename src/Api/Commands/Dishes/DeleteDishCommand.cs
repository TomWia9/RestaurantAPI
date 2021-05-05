using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RestaurantAPI.Commands.Dishes
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
