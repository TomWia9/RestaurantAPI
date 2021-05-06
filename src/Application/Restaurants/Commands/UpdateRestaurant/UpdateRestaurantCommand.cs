using System;
using Domain.Dto;
using MediatR;

namespace Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommand : RestaurantForManipulationDto, IRequest
    {
        public UpdateRestaurantCommand(Guid id, RestaurantForUpdateDto restaurant)
        {
            RestaurantId = id;
            RestaurantForUpdate = restaurant;
        }

        public Guid RestaurantId { get; set; }
        public RestaurantForUpdateDto RestaurantForUpdate { get; set; }
    }
}