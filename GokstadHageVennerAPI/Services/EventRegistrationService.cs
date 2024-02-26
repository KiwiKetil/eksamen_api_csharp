using GokstadHageVennerAPI.Mappers;
using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using GokstadHageVennerAPI.Repository;
using GokstadHageVennerAPI.Repository.Interfaces;
using GokstadHageVennerAPI.Services.Interfaces;
using StudentBloggAPI.Mappers.Interfaces;

namespace GokstadHageVennerAPI.Services;

public class EventRegistrationService : IEventRegistrationService
{
    private readonly IEventRegistrationRepository _eventRegistrationRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper<EventRegistration, EventRegistrationDTO> _eventRegistrationMapper;
    private readonly ILogger<EventRegistrationService> _logger;

    public EventRegistrationService(IEventRegistrationRepository eventRegistrationRepository, IEventRepository eventRepository  , IMemberRepository memberRepository, IMapper<EventRegistration, EventRegistrationDTO> eventRegistrationMapper, ILogger<EventRegistrationService> logger)
    {
        _eventRegistrationRepository = eventRegistrationRepository;
        _eventRepository = eventRepository;
        _memberRepository = memberRepository;
        _eventRegistrationMapper = eventRegistrationMapper;
        _logger = logger;
    }

    public async Task<ICollection<EventRegistrationDTO>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogDebug("Getting eventregistrations");

        var res = await _eventRegistrationRepository.GetAllAsync(page, pageSize);
        var dtos = res.Select(evt => _eventRegistrationMapper.MapToDTO(evt)).ToList();
        return dtos;
    }

    public async Task<EventRegistrationDTO?> GetByIdAsync(int id)
    {
        _logger.LogDebug("Getting eventregistration: {id}", id);

        var res = await _eventRegistrationRepository.GetByIdAsync(id);
        return res != null ? _eventRegistrationMapper.MapToDTO(res) : null;
    }

    public async Task<EventRegistrationDTO?> AddAsync(EventRegistrationDTO dto, int id, int loggedInUserId)
    {
        _logger.LogDebug("Registering for event");

        var eventToRegisterTo  = await _eventRepository.GetByIdAsync(id);
        if (eventToRegisterTo == null) return null;   

        var newEventRegistration = _eventRegistrationMapper.MapToEntity(dto);

        newEventRegistration.MemberId = loggedInUserId;
        newEventRegistration.EventId = eventToRegisterTo.Id;

        var res = await _eventRegistrationRepository.AddAsync(newEventRegistration);
        return res != null ? _eventRegistrationMapper.MapToDTO(res) : null;
    }

    public async Task<EventRegistrationDTO?> UpdateAsync(int id, EventRegistrationDTO dto, int loggedInUserId)
    {
        _logger.LogDebug("Updating eventregistration: {id}", id);

        var eventRegistrationToUpdate = await _eventRegistrationRepository.GetByIdAsync(id);
        var loginUser = await _memberRepository.GetByIdAsync(loggedInUserId);
        if (eventRegistrationToUpdate == null || loginUser == null) return null;

        if (eventRegistrationToUpdate.MemberId != loginUser.Id && !loginUser.IsAdminUser)
        {
            throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to update");
        }

        var evnt = _eventRegistrationMapper.MapToEntity(dto);
        evnt.Id = eventRegistrationToUpdate.Id;

        var res = await _eventRegistrationRepository.UpdateAsync(id, evnt);
        return res != null ? _eventRegistrationMapper.MapToDTO(res) : null;
    }

    public async Task<EventRegistrationDTO?> DeleteByIdAsync(int id, int loggedInUserId)
    {
        _logger?.LogDebug("Deleting eventregistration");

        var eventRegistrationToDelete = await _eventRegistrationRepository.GetByIdAsync(id);
        var loginUser = await _memberRepository.GetByIdAsync(loggedInUserId);

        if (eventRegistrationToDelete == null || loginUser == null) return null;

        if (eventRegistrationToDelete.MemberId != loginUser.Id && !loginUser.IsAdminUser)
        {
            throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to delete");
        }

        var res = await _eventRegistrationRepository.DeleteByIdAsync(id);
        return res != null ? _eventRegistrationMapper.MapToDTO(res) : null;
    }

    public async Task<ICollection<EventRegistrationDTO>> GetEventRegistrationsByEventId(int id, int page, int pageSize)
    {
        _logger?.LogDebug("Getting all registrations for event id: {id}", id);

        var res = await _eventRegistrationRepository.GetEventRegistrationsByEventId(id, page, pageSize);
        return res.Select(x => _eventRegistrationMapper.MapToDTO(x)).ToList();
    }

    public Task<EventRegistrationDTO?> AddAsync(EventRegistrationDTO dto, int loggedInUserId) // not used
    {
        throw new NotImplementedException();
    }
}
