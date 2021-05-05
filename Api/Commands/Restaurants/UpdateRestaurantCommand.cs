using System;
using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Commands.Restaurants
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
