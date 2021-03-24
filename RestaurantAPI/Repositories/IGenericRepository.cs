using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Repositories
{
    public interface IGenericRepository<in T> where T : class
    {
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<bool> SaveChangesAsync();
    }
}
