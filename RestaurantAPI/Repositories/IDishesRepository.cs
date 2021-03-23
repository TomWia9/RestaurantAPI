using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repositories
{
    public interface IDishesRepository
    {
        Task<IEnumerable<Dish>> GetAllAsync(int restaurantId);
        Task<Dish> GetAsync(int restaurantId, int id);
        Task<bool> AddAsync(Dish dish);
        Task<bool> DeleteAsync(Dish dish);
        Task<bool> UpdateAsync(Dish dish);
    }
}
