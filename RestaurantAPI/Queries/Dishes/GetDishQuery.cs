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
        public int RestaurantId { get; set; }
        public int DishId { get; set; }

        public GetDishQuery(int restaurantId, int dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }
    }
}
