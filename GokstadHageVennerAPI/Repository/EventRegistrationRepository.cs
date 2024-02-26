using GokstadHageVennerAPI.Data;
using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using GokstadHageVennerAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GokstadHageVennerAPI.Repository;

public class EventRegistrationRepository : IEventRegistrationRepository
{
    private readonly GokstadHageVennerDbContext _dbContext;
    private readonly ILogger<EventRegistrationRepository> _logger;

    public EventRegistrationRepository(GokstadHageVennerDbContext dbContext, ILogger<EventRegistrationRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<ICollection<EventRegistration>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogDebug("Getting eventregistrations from db");

        int itemsToSkip = (page - 1) * pageSize;

        return await _dbContext.EventRegistrations
            .OrderBy(u => u.Id)
            .Skip(itemsToSkip)
            .Take(pageSize)
            .Distinct()
            .ToListAsync();
    }

    public async Task<EventRegistration?> GetByIdAsync(int id)
    {
        _logger.LogDebug("Getting eventregistration: {id} from db", id);

        return await _dbContext.EventRegistrations.FindAsync(id);
    }

    public async  Task<EventRegistration?> AddAsync(EventRegistration entity)
    {
        _logger?.LogDebug("Adding eventregistration to db");

        await _dbContext.EventRegistrations.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }       

    public async Task<EventRegistration?> UpdateAsync(int id, EventRegistration entity)
    {
        _logger?.LogDebug("Updating eventregistration id: {id} in db", id);

        var eventRegistration = await _dbContext.EventRegistrations.FirstOrDefaultAsync(x => x.Id == id);
        if (eventRegistration == null)
            return null;

        eventRegistration.Status = string.IsNullOrEmpty(entity.Status) ? eventRegistration.Status : entity.Status;
        eventRegistration.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return eventRegistration;
    }

    public async Task<EventRegistration?> DeleteByIdAsync(int id)
    {
        _logger?.LogDebug("Deleting eventregistration from db");

        var res = await _dbContext.EventRegistrations.FindAsync(id);
        if (res != null)
        {
            _dbContext.EventRegistrations.Remove(res);
            await _dbContext.SaveChangesAsync();
            return res;
        }
        return null;
    }

    public async Task<ICollection<EventRegistration>> GetEventRegistrationsByEventId(int id, int page, int pageSize)
    {
        _logger.LogDebug("Getting all registrations for event id: {id} from db", id);

        int itemsToSkip = (page - 1) * pageSize;

        return await _dbContext.EventRegistrations
            .Where(c => c.EventId == id)
            .OrderBy(c => c.Id)
            .Skip(itemsToSkip)
            .Take(pageSize)
            .Distinct()
            .ToListAsync();
    }
}
