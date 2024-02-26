using GokstadHageVennerAPI.Models.DTOs;

namespace GokstadHageVennerAPI.Services.Interfaces;

public interface IEventRegistrationService : IService<EventRegistrationDTO>
{
    Task<ICollection<EventRegistrationDTO>> GetEventRegistrationsByEventId(int id, int page, int pageSize);
    Task<EventRegistrationDTO?> AddAsync(EventRegistrationDTO dto, int id, int loggedInUserId);
}
