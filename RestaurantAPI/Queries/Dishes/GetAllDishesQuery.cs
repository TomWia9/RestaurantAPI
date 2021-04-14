using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Shared.PagedList;

namespace RestaurantAPI.Queries.Dishes
{
    public class GetAllDishesQuery : IRequest<PagedList<DishDto>>
    {
        public Guid RestaurantId { get; set; }
        public DishesResourceParameters DishesResourceParameters { get; set; }

        public GetAllDishesQuery(Guid restaurantId, DishesResourceParameters dishesResourceParameters)
        {
            RestaurantId = restaurantId;
            DishesResourceParameters = dishesResourceParameters;
        }

    }
}
