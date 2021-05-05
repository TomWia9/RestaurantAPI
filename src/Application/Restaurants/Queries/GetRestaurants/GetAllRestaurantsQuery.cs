using Application.Common.Models;
using Domain.Dto;
using Domain.ResourceParameters;
using MediatR;

namespace Application.Restaurants.Queries.GetRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PagedList<RestaurantDto>>
    {
        public RestaurantsResourceParameters RestaurantsResourceParameters { get; set; }

        public GetAllRestaurantsQuery(RestaurantsResourceParameters restaurantsResourceParameters)
        {
            RestaurantsResourceParameters = restaurantsResourceParameters;
        }
    }
}
