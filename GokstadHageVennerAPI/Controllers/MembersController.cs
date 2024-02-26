using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace GokstadHageVennerAPI.Controllers;

[Route("api/v1/members")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly ILogger<MembersController> _logger;

    public MembersController(IMemberService memberService, ILogger<MembersController> logger)
    {
        _memberService = memberService;
        _logger = logger;
    }

    // https://localhost:7245/api/v1/members?page=1&pageSize=10
    [HttpGet(Name = "GetAllMembers")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetAllMembers(int page = 1, int pageSize = 10) 
    {
        _logger.LogDebug("Getting members");

        if (page < 1 || pageSize < 1 || pageSize > 50)
        {
            _logger.LogWarning("Invalid pagination parameters Page: {page}, PageSize: {pageSize}", page, pageSize);

            return BadRequest("Invalid pagination parameters - MIN page = 1, MAX pageSize = 50 ");
        }

        var res = await _memberService.GetAllAsync(page, pageSize);
        return Ok(res);
    }

    // https://localhost:7245/api/v1/members/1
    [HttpGet("{id}", Name = "GetMemberById")]
    public async Task<ActionResult<MemberDTO>> GetMemberById([FromRoute] int id)
    {
        _logger.LogDebug("Getting member: {id}", id);

        var res = await _memberService.GetByIdAsync(id);
        return res != null ? Ok(res) : NotFound("Could not find any member with this id");
    }

    // https://localhost:7245/api/v1/members/username?username=Yngve
    [HttpGet("username", Name = "GetMemberByUserName")]
    public async Task<ActionResult<MemberDTO>> GetMemberByUserName([FromQuery] string userName)
    {
        _logger.LogDebug("Getting member by username: {username}", userName);

        var res = await _memberService.GetByNameAsync(userName);
        return res != null ? Ok(res) : NotFound("Could not find any member with this username");
    }

    // https://localhost:7245/api/v1/members/register
    [HttpPost("register", Name = "RegisterMember")]
    public async Task<ActionResult<MemberDTO>> AddMember([FromBody] MemberRegistrationDTO dto)
    {
        _logger.LogDebug("Registering new member");

        var res = await _memberService.RegisterAsync(dto);

        return res != null
            ? Ok(res) : BadRequest("Could not register new member");
    }   

    // https://localhost:7245/api/v1/members/1
    [HttpPut("{id}", Name = "UpdateMember")]
    public async Task<ActionResult<MemberDTO>> UpdateMember([FromRoute] int id, [FromBody] MemberDTO dto)  
    {
        _logger.LogDebug("Updating member: {id}", id);

        int loggedInUserId = (int)this.HttpContext.Items["UserId"]!;

        var res = await _memberService.UpdateAsync(id, dto, loggedInUserId);
        return res != null ? Ok(res) : NotFound("Could not update member");
    }   

    // https://localhost:7245/api/v1/members/1
    [HttpDelete("{id}", Name = "DeleteMember")]
    public async Task<ActionResult<MemberDTO>> DeleteMember([FromRoute] int id)
    {
        _logger.LogDebug("Deleting member: {id}", id);

        int loggedInUserId = (int)this.HttpContext.Items["UserId"]!;

        var res = await _memberService.DeleteByIdAsync(id, loggedInUserId);
        return res != null ? Ok(res) : BadRequest("Could not delete member");
    }   
}
