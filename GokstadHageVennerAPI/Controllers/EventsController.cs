using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Services;
using GokstadHageVennerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace GokstadHageVennerAPI.Controllers;

[Route("api/v1/events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IEventService eventService ,ILogger<EventsController> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    // https://localhost:7245/api/v1/events?page=1&pageSize=10
    [HttpGet(Name = "GetAllEvents")]
    public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents(int page = 1, int pageSize = 10)
    {
        _logger.LogDebug("Getting events");

        if (page < 1 || pageSize < 1 || pageSize > 50)
        {
            _logger.LogWarning("Invalid pagination parameters Page: {page}, PageSize: {pageSize}", page, pageSize);

            return BadRequest("Invalid pagination parameters - MIN page = 1, MAX pageSize = 50 ");
        }

        var res = await _eventService.GetAllAsync(page, pageSize);
        return Ok(res);
    }

    // https://localhost:7245/api/v1/events/1
    [HttpGet("{eventId}", Name = "GetEventById")]
    public async Task<ActionResult<EventDTO>> GetEventById([FromRoute] int eventId)
    {
        _logger.LogDebug("Getting event: {id}", eventId);

        var res = await _eventService.GetByIdAsync(eventId);
        return res != null ? Ok(res) : NotFound("Could not find any event with this id");
    }
    
    // https://localhost:7245/api/v1/events/
    [HttpPost(Name = "AddEvent")]
    public async Task<ActionResult<EventDTO>> AddEvent([FromBody] EventDTO dto)
    {
        _logger.LogDebug("Registering new event");

        int loggedInUserId = (int)this.HttpContext.Items["UserId"]!;
        var res = await _eventService.AddAsync(dto, loggedInUserId);

        return res != null ? Ok(res) : BadRequest("Could not register new event");
    }

    // https://localhost:7245/api/v1/events/1
    [HttpPut("{eventId}", Name = "UpdateEvent")]
    public async Task<ActionResult<EventDTO>> UpdateEvent([FromRoute] int eventId, [FromBody] EventDTO dto)
    {
        _logger.LogDebug("Updating event: {id}", eventId);

        int loggedInUserId = (int)this.HttpContext.Items["UserId"]!;

        var res = await _eventService.UpdateAsync(eventId, dto, loggedInUserId);
        return res != null ? Ok(res) : NotFound("Could not update event");
    }

    // https://localhost:7245/api/v1/events/1
    [HttpDelete("{eventId}", Name = "DeleteEvent")]
    public async Task<ActionResult<EventDTO>> DeleteEvent([FromRoute] int eventId)
    {
        _logger.LogDebug("Deleting event: {id}", eventId);

        int loggedInUserId = (int)this.HttpContext.Items["UserId"]!;

        var res = await _eventService.DeleteByIdAsync(eventId, loggedInUserId);
        return res != null ? Ok(res) : BadRequest("Could not delete event");
    }
}
