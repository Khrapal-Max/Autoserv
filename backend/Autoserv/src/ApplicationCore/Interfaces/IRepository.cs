using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : class, IBaseEntity 
    {
        Task<T> CreateAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T item);
    }
}
