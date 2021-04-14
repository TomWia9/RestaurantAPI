using MediatR;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Shared.PagedList;

namespace RestaurantAPI.Queries.Restaurants
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
