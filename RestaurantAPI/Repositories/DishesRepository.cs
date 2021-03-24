using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repositories
{
    public class DishesRepository : GenericRepository<Dish>, IDishesRepository
    {
        public DishesRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Dish>> GetAllAsync(int restaurantId)
        {
            return await _context.Dishes.Where(d => d.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<Dish> GetAsync(int restaurantId, int id)
        {
            return await _context.Dishes.FirstOrDefaultAsync(d => d.RestaurantId == restaurantId && d.Id == id);
        }

    }
}
