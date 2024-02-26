using GokstadHageVennerAPI.Data;
using GokstadHageVennerAPI.Models.Entities;
using GokstadHageVennerAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace GokstadHageVennerAPI.Repository;

public class EventRepository : IEventRepository
{
    private readonly GokstadHageVennerDbContext _dbContext;
    private readonly ILogger<EventRepository> _logger;

    public EventRepository(GokstadHageVennerDbContext dbContext, ILogger<EventRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ICollection<Event>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogDebug("Getting events from db");

        int itemsToSkip = (page - 1) * pageSize;

        return await _dbContext.Events
            .OrderBy(u => u.Id)
            .Skip(itemsToSkip)
            .Take(pageSize)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        _logger.LogDebug("Getting event: {id} from db", id);

        return await _dbContext.Events.FindAsync(id);
    }

    public async Task<Event?> AddAsync(Event entity)
    {
        _logger?.LogDebug("Adding event to db");

        await _dbContext.Events.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Event?> UpdateAsync(int id, Event entity)
    {
        _logger?.LogDebug("Updating event id: {id} in db", id);

        var evnt = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);
        if (evnt == null)
            return null;

        evnt.EventType = string.IsNullOrEmpty(entity.EventType) ? evnt.EventType : entity.EventType;
        evnt.EventName = string.IsNullOrEmpty(evnt.EventName) ? evnt.EventName : entity.EventName;
        evnt.Description = string.IsNullOrEmpty(evnt.Description) ? evnt.Description : entity.Description;
        evnt.Updated = DateTime.Now;

        if (entity.EventDate != default)
            evnt.EventDate = entity.EventDate;

        if (entity.EventTime != default)
            evnt.EventTime = entity.EventTime;              

        await _dbContext.SaveChangesAsync();

        return evnt;
    }

    public async Task<Event?> DeleteByIdAsync(int id)
    {
        _logger?.LogDebug("Deleting event id: {id} from db", id);

        var res = await _dbContext.Events.FindAsync(id);
        if (res != null)
        {
            _dbContext.Events.Remove(res);
            await _dbContext.SaveChangesAsync();
            return res;
        }
        return null;
    }
}
