using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;
using RestaurantAPI.Shared.Enums;
using RestaurantAPI.Shared.PagedList;

namespace RestaurantAPI.Repositories
{
    public class DishesRepository : GenericRepository<Dish>, IDishesRepository
    {
        public DishesRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Dish>> GetAllAsync(int restaurantId, DishesResourceParameters dishesResourceParameters)
        {
            if (dishesResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(dishesResourceParameters));
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

            if (!string.IsNullOrWhiteSpace(dishesResourceParameters.SortBy))
            {
                collection = SortDishes(collection, dishesResourceParameters.SortBy, dishesResourceParameters.SortDirection);
            }

            return await PagedList<Dish>.ToPagedListAsync(collection,
                dishesResourceParameters.PageNumber, dishesResourceParameters.PageSize);
        }

        public async Task<Dish> GetAsync(int restaurantId, int id)
        {
            return await _context.Dishes.FirstOrDefaultAsync(d => d.RestaurantId == restaurantId && d.Id == id);
        }

        private static IQueryable<Dish> SortDishes(IQueryable<Dish> collection, string sortBy, SortDirection sortDirection)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Dish, object>>>
            {
                { nameof(Dish.Name), d => d.Name },
                { nameof(Dish.Description), d => d.Description },
            };

            var selectedColumn = columnsSelector[sortBy];

            collection = sortDirection == SortDirection.ASC
                ? collection.OrderBy(selectedColumn)
                : collection.OrderByDescending(selectedColumn);

            return collection;
        }

    }
}
