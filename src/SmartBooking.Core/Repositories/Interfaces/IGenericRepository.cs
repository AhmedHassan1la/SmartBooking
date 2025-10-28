using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SmartBooking.Core.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, object>>[] Includes);

        IEnumerable<T> GetAll(
            Expression<Func<T, object>>[] Includes);

        Task<T> GetAsync(int id);

        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(T entity);
    }
}