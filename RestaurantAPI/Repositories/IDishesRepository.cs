using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;
using RestaurantAPI.Shared.PagedList;

namespace RestaurantAPI.Repositories
{
    public interface IDishesRepository : IGenericRepository<Dish>
    {
        Task<PagedList<Dish>> GetAllAsync(int restaurantId, DishesResourceParameters dishesResourceParameters);
        Task<Dish> GetAsync(int restaurantId, int id);
      
    }
}
