using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;

namespace RestaurantAPI.Queries
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
    {
        public RestaurantsResourceParameters RestaurantsResourceParameters { get; set; }

        public GetAllRestaurantsQuery(RestaurantsResourceParameters restaurantsResourceParameters)
        {
            RestaurantsResourceParameters = restaurantsResourceParameters;
        }
    }
}
