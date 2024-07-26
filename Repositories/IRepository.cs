using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagerApp.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
