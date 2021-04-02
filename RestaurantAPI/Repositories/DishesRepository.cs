﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.ResourceParameters;
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

        public async Task<IEnumerable<Dish>> GetAllAsync(int restaurantId, DishesResourceParameters dishesResourceParameters)
        {
            if (dishesResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(dishesResourceParameters));
            }

            if (dishesResourceParameters.MaximumPrice == null
                && string.IsNullOrWhiteSpace(dishesResourceParameters.SearchQuery))
            {
                return await GetAllAsync(restaurantId);
            }

            var collection = _context.Dishes.Where(d => d.RestaurantId == restaurantId);

            if (dishesResourceParameters.MaximumPrice != null)
            {
                collection = collection.Where(d => d.Price <= dishesResourceParameters.MaximumPrice);
            }

            if (!string.IsNullOrWhiteSpace(dishesResourceParameters.SearchQuery))
            {
                var searchQuery = dishesResourceParameters.SearchQuery.Trim();

                collection = collection.Where(d =>
                    d.Name.Contains(searchQuery) ||
                    d.Description.Contains(searchQuery));

            }

            return await collection.ToListAsync();
        }

        public async Task<Dish> GetAsync(int restaurantId, int id)
        {
            return await _context.Dishes.FirstOrDefaultAsync(d => d.RestaurantId == restaurantId && d.Id == id);
        }

    }
}
