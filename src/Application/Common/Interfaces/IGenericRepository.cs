using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGenericRepository<in T> where T : class
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}