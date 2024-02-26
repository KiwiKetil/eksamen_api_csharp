namespace GokstadHageVennerAPI.Repository.Interfaces;

public interface IRepository<T> where T : class
{
    Task<ICollection<T>> GetAllAsync(int page, int pageSize);
    Task<T?> GetByIdAsync(int id);
    Task<T?> AddAsync(T entity);
    Task<T?> UpdateAsync(int id, T entity);
    Task<T?> DeleteByIdAsync(int id);  
}
