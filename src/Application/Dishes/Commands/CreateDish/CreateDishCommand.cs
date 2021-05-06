using System;
using Domain.Dto;
using MediatR;

namespace Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommand : IRequest<DishDto>
    {
        public CreateDishCommand(Guid restaurantId, DishForCreationDto dishForCreation)
        {
            DishForCreation = dishForCreation;
            RestaurantId = restaurantId;
        }

        public Guid RestaurantId { get; set; }

        public DishForCreationDto DishForCreation { get; set; }
    }
}