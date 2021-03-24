using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantsRepository
    {
        public RestaurantRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.Include(r => r.Address).ToListAsync();
        }

        public async Task<Restaurant> GetAsync(int id)
        {
            return await _context.Restaurants.Include(r => r.Address).FirstOrDefaultAsync(r => r.Id == id);
        }

    }
}
