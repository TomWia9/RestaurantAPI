using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Queries.Restaurants
{
    public class GetRestaurantQuery : IRequest<RestaurantDto>
    {
        public int RestaurantId { get; set; }

        public GetRestaurantQuery(int id)
        {
            RestaurantId = id;
        }
    }
}
