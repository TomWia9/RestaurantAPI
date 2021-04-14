using System;
using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Queries.Restaurants
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
