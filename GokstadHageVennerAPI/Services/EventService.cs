using GokstadHageVennerAPI.Mappers;
using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using GokstadHageVennerAPI.Repository;
using GokstadHageVennerAPI.Repository.Interfaces;
using StudentBloggAPI.Mappers.Interfaces;

namespace GokstadHageVennerAPI.Services.Interfaces;

public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;
    private readonly IEventRepository _eventRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper<Event, EventDTO> _eventMapper;

    public EventService(IEventRepository eventRepository, IMemberRepository memberRepository ,IMapper<Event, EventDTO> eventMapper, ILogger<EventService> logger)
    {
        _logger = logger;
        _eventRepository = eventRepository;
        _memberRepository = memberRepository;
        _eventMapper = eventMapper;
    }

    public async Task<ICollection<EventDTO>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogDebug("Getting events");

        var res = await _eventRepository.GetAllAsync(page, pageSize);
        var dtos = res.Select(evt => _eventMapper.MapToDTO(evt)).ToList();
        return dtos;
    }

    public async Task<EventDTO?> GetByIdAsync(int id)
    {
        _logger.LogDebug("Getting event: {id}", id);

        var res = await _eventRepository.GetByIdAsync(id);
        return res != null ? _eventMapper.MapToDTO(res) : null;
    }

    public async Task<EventDTO?> AddAsync(EventDTO dto, int loggedInUserId)
    {
        _logger.LogDebug("Registering new event");

        var newEvent = _eventMapper.MapToEntity(dto);
        newEvent.MemberId= loggedInUserId;
        var res = await _eventRepository.AddAsync(newEvent);
        return res != null ? _eventMapper.MapToDTO(res) : null;        
    }
      
    public async Task<EventDTO?> UpdateAsync(int id, EventDTO dto, int loggedInUserId)
    {
        _logger.LogDebug("Updating event: {id}", id);

        var eventToUpdate = await _eventRepository.GetByIdAsync(id);
        var loginUser = await _memberRepository.GetByIdAsync(loggedInUserId);
        if (eventToUpdate == null || loginUser == null) return null;

        if (eventToUpdate.MemberId != loginUser.Id && !loginUser.IsAdminUser)
        {
            throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to update");
        }

        var evnt = _eventMapper.MapToEntity(dto);
        evnt.Id = eventToUpdate.Id;

        var res = await _eventRepository.UpdateAsync(id, evnt);
        return res != null ? _eventMapper.MapToDTO(res) : null;
    }

    public async Task<EventDTO?> DeleteByIdAsync(int id, int loggedInUserId)
    {
        _logger.LogDebug("Deleting event: {id}", id);

        var eventToDelete = await _eventRepository.GetByIdAsync(id);
        var loginUser = await _memberRepository.GetByIdAsync(loggedInUserId);

        if (eventToDelete == null || loginUser == null) return null;

        if (eventToDelete.MemberId != loginUser.Id && !loginUser.IsAdminUser)
        {
            throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to delete");
        }

        var res = await _eventRepository.DeleteByIdAsync(id);
        return res != null ? _eventMapper.MapToDTO(res) : null;
    }   
}
