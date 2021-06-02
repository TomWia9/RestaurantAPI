using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Domain.ResourceParameters;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DishesRepository : GenericRepository<Dish>, IDishesRepository
    {
        public DishesRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Dish>> GetAllAsync(Guid restaurantId,
            DishesResourceParameters dishesResourceParameters)
        {
            if (dishesResourceParameters == null) throw new ArgumentNullException(nameof(dishesResourceParameters));

            var collection = _context.Dishes.Where(d => d.RestaurantId == restaurantId);

            //filtering
            if (!string.IsNullOrWhiteSpace(dishesResourceParameters.Name))
                collection = collection.Where(d => d.Name == dishesResourceParameters.Name);

            if (dishesResourceParameters.MaximumPrice != null)
                collection = collection.Where(d => d.Price <= dishesResourceParameters.MaximumPrice);

            if (!string.IsNullOrWhiteSpace(dishesResourceParameters.SearchQuery))
            {
                var searchQuery = dishesResourceParameters.SearchQuery.Trim();

                collection = collection.Where(d =>
                    d.Name.Contains(searchQuery) ||
                    d.Description.Contains(searchQuery));
            }

            //sorting
            if (string.IsNullOrWhiteSpace(dishesResourceParameters.SortBy))
                //default sorting
                return await PagedList<Dish>.ToPagedListAsync(collection.OrderBy(d => d.Name),
                    dishesResourceParameters.PageNumber, dishesResourceParameters.PageSize);

            //custom sorting
            collection = SortDishes(collection, dishesResourceParameters.SortBy,
                dishesResourceParameters.SortDirection);

            //pagination
            return await PagedList<Dish>.ToPagedListAsync(collection,
                dishesResourceParameters.PageNumber, dishesResourceParameters.PageSize);
        }

        public async Task<Dish> GetAsync(Guid restaurantId, Guid id)
        {
            return await _context.Dishes.FirstOrDefaultAsync(d => d.RestaurantId == restaurantId && d.Id == id);
        }

        public async Task<bool> RestaurantExists(Guid restaurantId)
        {
            return await _context.Restaurants.AnyAsync(r => r.Id == restaurantId);
        }

        private static IQueryable<Dish> SortDishes(IQueryable<Dish> collection, string sortBy,
            SortDirection sortDirection)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Dish, object>>>
            {
                {nameof(Dish.Name), d => d.Name},
                {nameof(Dish.Description), d => d.Description}
            };

            var selectedColumn = columnsSelector[sortBy.FirstCharToUpper()];

            collection = sortDirection == SortDirection.ASC
                ? collection.OrderBy(selectedColumn)
                : collection.OrderByDescending(selectedColumn);

            return collection;
        }
    }
}