using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using GokstadHageVennerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace GokstadHageVennerAPI.Controllers;

[Route("api/v1/eventregistrations")]
[ApiController]
public class EventRegistrationsController : ControllerBase
{
    private readonly IEventRegistrationService _eventRegistrationService;
    private readonly ILogger<EventRegistrationsController> _logger;

    public EventRegistrationsController(IEventRegistrationService eventRegistrationService, ILogger<EventRegistrationsController> logger)
    {
        _eventRegistrationService = eventRegistrationService;
        _logger = logger;
    }

    // https://localhost:7245/api/v1/eventregistrations?page=1&pageSize=10
    [HttpGet(Name = "GetEventRegistrations")]
    public async Task<ActionResult<IEnumerable<EventRegistrationDTO>>> GetAllEventsRegistrations(int page = 1, int pageSize = 10)
    {
        _logger.LogDebug("Getting eventregistrations");

        if (page < 1 || pageSize < 1 || pageSize > 50)
        {
            _logger.LogWarning("Invalid pagination parameters Page: {page}, PageSize: {pageSize}", page, pageSize);

            return BadRequest("Invalid pagination parameters - MIN page = 1, MAX pageSize = 50 ");
        }

        var res = await _eventRegistrationService.GetAllAsync(page, pageSize);
        return Ok(res);
    }

    // https://localhost:7245/api/v1/eventregistrations/1
    [HttpGet("{registrationId}", Name = "GetEventRegistrationById")]
    public async Task<ActionResult<EventRegistrationDTO>> GetEventRegistrationById([FromRoute] int registrationId)
    {
        _logger.LogDebug("Getting eventregistration: {id}", registrationId);

        var res = await _eventRegistrationService.GetByIdAsync(registrationId);
        return res != null ? Ok(res) : NotFound("Could not find any eventregistration with this id");
    }

    // https://localhost:7245/api/v1/eventregistrations/1
    [HttpPost("{eventId}", Name = "RegisterForEvent")]
    public async Task<ActionResult<EventRegistrationDTO>> RegisterForEvent([FromBody] EventRegistrationDTO dto, [FromRoute] int eventId)
    {
        _logger.LogDebug("Registering for event");

        int loggedInUserId = (int)this.HttpContext.Items["UserId"]!;
        var res = await _eventRegistrationService.AddAsync(dto, eventId, loggedInUserId);

        return res != null ? Ok(res) : BadRequest("Could not register for event");
    }

    // https://localhost:7245/api/v1/eventregistrations/1
    [HttpPut("{registrationId}", Name = "UpdateEventRegistration")]
    public async Task<ActionResult<EventRegistrationDTO>> UpdateEventRegistration([FromBody] EventRegistrationDTO dto, [FromRoute] int registrationId)
    {
        _logger.LogDebug("Updating eventregistration: {id}", registrationId);

        int loggedInUserId = (int)this.HttpContext.Items["UserId"]!;

        var res = await _eventRegistrationService.UpdateAsync(registrationId, dto, loggedInUserId);
        return res != null ? Ok(res) : NotFound("Could not update eventregistration");
    }

    // https://localhost:7245/api/v1/eventregistrations/1
    [HttpDelete("{registrationId}", Name = "DeleteEventRegistration")]
    public async Task<ActionResult<EventRegistrationDTO>> DeleteEventRegistration([FromRoute ]int registrationId)
    {
        _logger.LogDebug("Deleting eventregistration: {id}", registrationId);

        int loggedInUserId = (int)this.HttpContext.Items["UserId"]!;

        var res = await _eventRegistrationService.DeleteByIdAsync(registrationId, loggedInUserId);
        return res != null ? Ok(res) : BadRequest("Could not delete eventregistration");
    }

    //https://localhost:7245/api/v1/eventregistrations/2/registrations?page=1&pageSize=10
    [HttpGet("{eventId}/registrations", Name = "GetEventRegistrationsByEventId")]
    public async Task<ActionResult<IEnumerable<EventRegistrationDTO>>> GetEventRegistrationsByEventId([FromRoute] int eventId, int page = 1, int pageSize = 10)
    {
        _logger.LogDebug("Getting eventregistrations by eventid: {id}", eventId);

        if (page < 1 || pageSize < 1 || pageSize > 50)
        {
            _logger.LogWarning("Invalid pagination parameters for event id {id}. Page: {page}, PageSize: {pageSize}", eventId, page, pageSize);

            return BadRequest("Invalid pagination parameters - MIN page = 1, MAX pageSize = 50 ");
        }

        var res = await _eventRegistrationService.GetEventRegistrationsByEventId(eventId, page, pageSize);
        return Ok(res);
    }
}
