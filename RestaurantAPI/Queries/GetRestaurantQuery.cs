using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Queries
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
