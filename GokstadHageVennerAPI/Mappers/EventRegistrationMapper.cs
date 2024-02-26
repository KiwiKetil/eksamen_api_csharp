using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using StudentBloggAPI.Mappers.Interfaces;

namespace GokstadHageVennerAPI.Mappers;

public class EventRegistrationMapper : IMapper<EventRegistration, EventRegistrationDTO>
{
    public EventRegistrationDTO MapToDTO(EventRegistration entity)
    {
        return new EventRegistrationDTO()
        {
            Id = entity.Id,
            EventId = entity.EventId,
            MemberId = entity.MemberId,
            Status = entity.Status,
            Created = entity.Created,
            Updated = entity.Updated,
        };
    }

    public EventRegistration MapToEntity(EventRegistrationDTO dto)
    {
        var dtNow = DateTime.Now;
        return new EventRegistration()
        {
            Id = dto.Id,
            EventId = dto.EventId,
            MemberId = dto.MemberId,
            Status = dto.Status,
            Created = dto.Created,
            Updated = dto.Updated,
        };
    }
}
