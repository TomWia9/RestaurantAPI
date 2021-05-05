using Domain.Dto;
using MediatR;

namespace Application.Restaurants.Commands.CreateRestaurant
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
