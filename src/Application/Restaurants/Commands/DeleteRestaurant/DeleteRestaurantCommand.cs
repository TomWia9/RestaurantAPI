using System;
using MediatR;

namespace Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand : IRequest
    {
        public Guid RestaurantId { get; set; }

        public DeleteRestaurantCommand(Guid id)
        {
            RestaurantId = id;
        }
    }
}
