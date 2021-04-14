using System;
using MediatR;

namespace RestaurantAPI.Commands.Restaurants
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
