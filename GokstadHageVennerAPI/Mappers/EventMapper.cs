using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using StudentBloggAPI.Mappers.Interfaces;

namespace GokstadHageVennerAPI.Mappers;

public class EventMapper : IMapper<Event, EventDTO>
{
    public EventDTO MapToDTO(Event entity)
    {
        return new EventDTO()
        {
            Id = entity.Id,
            MemberId = entity.MemberId,
            EventType = entity.EventType,
            EventName = entity.EventName,
            Description = entity.Description,
            EventDate = entity.EventDate,
            EventTime = entity.EventTime,
            Created = entity.Created,
            Updated = entity.Updated,   
           
        };
    }

    public Event MapToEntity(EventDTO dto)
    {
        var dtNow = DateTime.Now;
        return new Event()
        {
            Id = dto.Id,
            MemberId = dto.MemberId,
            EventType = dto.EventType,
            EventName = dto.EventName,
            Description = dto.Description,
            EventDate = dto.EventDate,
            EventTime = dto.EventTime,
            Created = dtNow,
            Updated = dtNow
        };
    }
}
