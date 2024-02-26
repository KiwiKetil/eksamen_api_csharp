using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;

namespace GokstadHageVennerAPI.Repository.Interfaces
{
    public interface IEventRegistrationRepository : IRepository<EventRegistration>
    {
        Task<ICollection<EventRegistration>> GetEventRegistrationsByEventId(int id, int page, int pageSize);
    }
}
