using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Commands.Dishes
{
    public class UpdateDishCommand : IRequest
    {
        public int RestaurantId { get; set; }
        public int DishId { get; set; }
        public DishForUpdateDto DishForUpdate { get; set; }

        public UpdateDishCommand(int restaurantId, int dishId, DishForUpdateDto dishForUpdate)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
            DishForUpdate = dishForUpdate;
        }
    }
}
