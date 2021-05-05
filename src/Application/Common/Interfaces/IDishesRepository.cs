using System;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities;
using Domain.ResourceParameters;

namespace Application.Common.Interfaces
{
    public interface IDishesRepository : IGenericRepository<Dish>
    {
        Task<PagedList<Dish>> GetAllAsync(Guid restaurantId, DishesResourceParameters dishesResourceParameters);
        Task<Dish> GetAsync(Guid restaurantId, Guid id);
        Task<bool> RestaurantExists(Guid restaurantId);

    }
}
