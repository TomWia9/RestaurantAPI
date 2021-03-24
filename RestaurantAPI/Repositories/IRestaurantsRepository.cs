using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repositories
{
    public interface IRestaurantsRepository : IGenericRepository<Restaurant>
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant> GetAsync(int id);
    }
}
