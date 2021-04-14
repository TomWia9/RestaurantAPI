using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;

namespace RestaurantAPI.Queries.Dishes
{
    public class GetDishQuery : IRequest<DishDto>
    {
        public Guid RestaurantId { get; set; }
        public Guid DishId { get; set; }

        public GetDishQuery(Guid restaurantId, Guid dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }
    }
}
