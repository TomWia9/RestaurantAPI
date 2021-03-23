using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repositories
{
    public class DishesRepository : IDishesRepository
    {
        private readonly RestaurantDbContext _context;

        public DishesRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> GetAllAsync(int restaurantId)
        {
            return await _context.Dishes.Where(d => d.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<Dish> GetAsync(int restaurantId, int id)
        {
            return await _context.Dishes.FirstOrDefaultAsync(d => d.RestaurantId == restaurantId && d.Id == id);
        }

        public async Task<bool> AddAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Dish dish)
        {
             _context.Dishes.Remove(dish);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Dish dish)
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
