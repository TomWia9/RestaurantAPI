using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Commands.Dishes
{
    public class CreateDishCommand : IRequest<DishDto>
    {
        public Guid RestaurantId { get; set; }

        public CreateDishCommand(Guid restaurantId, DishForCreationDto dishForCreation)
        {
            DishForCreation = dishForCreation;
            RestaurantId = restaurantId;
        }

        public DishForCreationDto DishForCreation { get; set; }

    }
}
