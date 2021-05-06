using System;
using MediatR;

namespace Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand : IRequest
    {
        public DeleteRestaurantCommand(Guid id)
        {
            RestaurantId = id;
        }

        public Guid RestaurantId { get; set; }
    }
}