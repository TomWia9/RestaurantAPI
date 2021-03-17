using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant> GetAsync(int id);
        Task<bool> AddAsync(Restaurant restaurant);
        Task<bool> DeleteAsync(Restaurant restaurant);
        void Update(Restaurant restaurant);
    }
}
