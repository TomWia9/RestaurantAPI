using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repositories
{
    public interface IDishesRepository : IGenericRepository<Dish>
    {
        Task<IEnumerable<Dish>> GetAllAsync(int restaurantId);
        Task<IEnumerable<Dish>> GetAllAsync(int restaurantId, DishesResourceParameters dishesResourceParameters);
        Task<Dish> GetAsync(int restaurantId, int id);
      
    }
}
