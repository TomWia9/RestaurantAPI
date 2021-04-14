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
        public Guid RestaurantId { get; set; }
        public Guid DishId { get; set; }
        public DishForUpdateDto DishForUpdate { get; set; }

        public UpdateDishCommand(Guid restaurantId, Guid dishId, DishForUpdateDto dishForUpdate)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
            DishForUpdate = dishForUpdate;
        }
    }
}
