using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Data.Dto;
using RestaurantAPI.Data.ResourceParameters;
using RestaurantAPI.Models;
using RestaurantAPI.Shared.PagedList;

namespace RestaurantAPI.Repositories
{
    public interface IRestaurantsRepository : IGenericRepository<Restaurant>
    {
        Task<PagedList<Restaurant>> GetAllAsync(RestaurantsResourceParameters restaurantsResourceParameters);
        Task<Restaurant> GetAsync(int id);
    }
}
