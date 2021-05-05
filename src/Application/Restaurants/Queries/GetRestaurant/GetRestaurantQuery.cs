using System;
using Domain.Dto;
using MediatR;

namespace Application.Restaurants.Queries.GetRestaurant
{
    public class GetRestaurantQuery : IRequest<RestaurantDto>
    {
        public Guid RestaurantId { get; set; }

        public GetRestaurantQuery(Guid id)
        {
            RestaurantId = id;
        }
    }
}
