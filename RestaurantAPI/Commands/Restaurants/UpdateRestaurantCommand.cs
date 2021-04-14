using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Commands.Restaurants
{
    public class UpdateRestaurantCommand : RestaurantForManipulationDto, IRequest
    {
        public int RestaurantId { get; set; }
        public RestaurantForUpdateDto RestaurantForUpdate { get; set; }

        public UpdateRestaurantCommand(int id, RestaurantForUpdateDto restaurant)
        {
            RestaurantId = id;
            RestaurantForUpdate = restaurant;
        }
    }
}
