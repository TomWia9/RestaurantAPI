using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repositories
{
    public class RestaurantRepository : IRestaurantsRepository
    {
        private readonly RestaurantDbContext _context;

        public RestaurantRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetAsync(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public async Task<bool> Add(Restaurant restaurant)
        {
           await _context.Restaurants.AddAsync(restaurant);
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(RestaurantForUpdateDto restaurant)
        {
            //no code in this implementation
        }
    }
}
