using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Shared.PagedList;

namespace RestaurantAPI.Queries
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
