using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Repositories
{
    interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<bool> SaveChangesAsync();
    }
}
