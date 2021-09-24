using Application.Common.Models;
using Domain.Dto;
using Domain.ResourceParameters;
using MediatR;

namespace Application.Restaurants.Queries.GetRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PagedList<RestaurantDto>>
    {
        public GetAllRestaurantsQuery(RestaurantsResourceParameters restaurantsResourceParameters)
        {
            RestaurantsResourceParameters = restaurantsResourceParameters;
        }

        public RestaurantsResourceParameters RestaurantsResourceParameters { get; set; }
    }
}