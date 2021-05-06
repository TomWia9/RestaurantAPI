using Domain.Dto;
using MediatR;

namespace Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommand : IRequest<RestaurantDto>
    {
        public CreateRestaurantCommand(RestaurantForCreationDto restaurant)
        {
            RestaurantForCreation = restaurant;
        }

        public RestaurantForCreationDto RestaurantForCreation { get; set; }
    }
}