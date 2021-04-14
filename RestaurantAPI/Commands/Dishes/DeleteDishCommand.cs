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
        public int RestaurantId { get; set; }
        public int DishId { get; set; }

        public DeleteDishCommand(int restaurantId, int dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }
    }
}
