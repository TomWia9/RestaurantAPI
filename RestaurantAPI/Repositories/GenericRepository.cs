using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Models;

namespace RestaurantAPI.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        protected readonly RestaurantDbContext _context;

        protected GenericRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _context.SaveChangesAsync();
        }

    }
}
