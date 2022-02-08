namespace ApplicationCore.Interfaces
{
    public interface IBaseService<T, U> where T : class where U : class
    {
        Task<IEnumerable<U>> GetAllAsync();

        Task<U> GetByIdAsync(int id);

        Task<U> CreateAsync(T item);

        Task<U> UpdateAsync(U item);

        Task DeleteAsync(int id);
    }
}
