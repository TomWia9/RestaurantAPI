using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;
using RestaurantAPI.Shared.Enums;
using RestaurantAPI.Shared.PagedList;

namespace RestaurantAPI.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantsRepository
    {
        public RestaurantRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Restaurant>> GetAllAsync(RestaurantsResourceParameters restaurantsResourceParameters)
        {
            if (restaurantsResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(restaurantsResourceParameters));
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

            if (!string.IsNullOrWhiteSpace(restaurantsResourceParameters.SortBy))
            {
               collection = SortRestaurants(collection, restaurantsResourceParameters.SortBy, restaurantsResourceParameters.SortDirection);
            }

            return await PagedList<Restaurant>.ToPagedListAsync(collection,
                restaurantsResourceParameters.PageNumber, restaurantsResourceParameters.PageSize);

        }

        public async Task<Restaurant> GetAsync(int id)
        {
            return await _context.Restaurants.Include(r => r.Address).FirstOrDefaultAsync(r => r.Id == id);
        }

        private static IQueryable<Restaurant> SortRestaurants(IQueryable<Restaurant> collection, string sortBy, SortDirection sortDirection)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category },
                { nameof(Restaurant.Address.City), r => r.Address.City },
            };

            var selectedColumn = columnsSelector[sortBy];

            collection = sortDirection == SortDirection.ASC
                ? collection.OrderBy(selectedColumn)
                : collection.OrderByDescending(selectedColumn);

            return collection;
        }

    }
}
