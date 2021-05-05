using System;
using Domain.Dto;
using MediatR;

namespace Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommand : RestaurantForManipulationDto, IRequest
    {
        public Guid RestaurantId { get; set; }
        public RestaurantForUpdateDto RestaurantForUpdate { get; set; }

        public UpdateRestaurantCommand(Guid id, RestaurantForUpdateDto restaurant)
        {
            RestaurantId = id;
            RestaurantForUpdate = restaurant;
        }
    }
}
