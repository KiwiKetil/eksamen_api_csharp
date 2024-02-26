using GokstadHageVennerAPI.Models.Entities;

namespace GokstadHageVennerAPI.Repository.Interfaces;

public interface IMemberRepository : IRepository<Member>
{
    Task<Member?> GetByNameAsync(string userName);
}
