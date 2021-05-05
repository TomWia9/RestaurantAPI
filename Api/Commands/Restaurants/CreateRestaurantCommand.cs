using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Commands.Restaurants
{
    public class CreateRestaurantCommand : IRequest<RestaurantDto>
    {
        public RestaurantForCreationDto RestaurantForCreation { get; set; }

        public CreateRestaurantCommand(RestaurantForCreationDto restaurant)
        {
            RestaurantForCreation = restaurant;
        }
    }
}
