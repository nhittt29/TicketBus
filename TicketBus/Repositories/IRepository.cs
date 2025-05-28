using System.Linq.Expressions;

namespace TicketBus.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }
}
