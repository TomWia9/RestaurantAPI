using System;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities;
using Domain.ResourceParameters;

namespace Application.Common.Interfaces
{
    public interface IRestaurantsRepository : IGenericRepository<Restaurant>
    {
        Task<PagedList<Restaurant>> GetAllAsync(RestaurantsResourceParameters restaurantsResourceParameters);
        Task<Restaurant> GetAsync(Guid id);
    }
}
