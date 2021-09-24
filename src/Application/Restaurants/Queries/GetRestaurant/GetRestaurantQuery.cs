using System;
using Domain.Dto;
using MediatR;

namespace Application.Restaurants.Queries.GetRestaurant
{
    public class GetRestaurantQuery : IRequest<RestaurantDto>
    {
        public GetRestaurantQuery(Guid id)
        {
            RestaurantId = id;
        }

        public Guid RestaurantId { get; set; }
    }
}