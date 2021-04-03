using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
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

        public async Task<IEnumerable<Restaurant>> GetAllAsync(RestaurantsResourceParameters restaurantsResourceParameters)
        {
            if (restaurantsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(restaurantsResourceParameters));
            }

            if (restaurantsResourceParameters.HasDelivery == null
                && string.IsNullOrWhiteSpace(restaurantsResourceParameters.SearchQuery)
                && (restaurantsResourceParameters.PageSize == 0
                    || restaurantsResourceParameters.PageNumber == 0))
            {
                return await GetAllAsync();
            }

            var collection = _context.Restaurants.Include(r => r.Address) as IQueryable<Restaurant>;

            if (restaurantsResourceParameters.HasDelivery != null)
            {
                collection = collection.Where(r => r.HasDelivery == restaurantsResourceParameters.HasDelivery);
            }

            if (!string.IsNullOrWhiteSpace(restaurantsResourceParameters.SearchQuery))
            {
                var searchQuery = restaurantsResourceParameters.SearchQuery.Trim();

                collection = collection.Where(r =>
                    r.Name.Contains(searchQuery) ||
                    r.Description.Contains(searchQuery) ||
                    r.Category.Contains(searchQuery) ||
                    r.Address.City.Contains(searchQuery));
            }

            if (!(restaurantsResourceParameters.PageSize == 0 && restaurantsResourceParameters.PageNumber == 0))
            {
                return await collection.Skip(restaurantsResourceParameters.PageSize * (restaurantsResourceParameters.PageNumber - 1))
                    .Take(restaurantsResourceParameters.PageSize)
                    .ToListAsync();
            }

           return await collection.ToListAsync();

        }

        public async Task<Restaurant> GetAsync(int id)
        {
            return await _context.Restaurants.Include(r => r.Address).FirstOrDefaultAsync(r => r.Id == id);
        }

    }
}
