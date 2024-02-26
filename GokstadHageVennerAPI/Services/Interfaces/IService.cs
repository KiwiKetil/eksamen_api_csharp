using GokstadHageVennerAPI.Models.DTOs;

namespace GokstadHageVennerAPI.Services.Interfaces;

public interface IService<T> where T : class
{
    Task<ICollection<T>> GetAllAsync(int page, int pageSize);
    Task<T?> GetByIdAsync(int id);
    Task<T?> AddAsync(T dto, int loggedInUserId);
    Task<T?> UpdateAsync(int id, T dto, int loggedInUserId); 
    Task<T?> DeleteByIdAsync(int id, int loggedInUserId);
}
