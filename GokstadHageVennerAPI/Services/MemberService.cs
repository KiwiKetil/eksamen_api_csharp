using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using GokstadHageVennerAPI.Repository.Interfaces;
using GokstadHageVennerAPI.Services.Interfaces;
using StudentBloggAPI.Mappers.Interfaces;

namespace GokstadHageVennerAPI.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper<Member, MemberDTO> _memberMapper;
    private readonly IMapper<Member, MemberRegistrationDTO> _memberRegistrationMapper;
    private readonly ILogger<MemberService> _logger;

    public MemberService(IMemberRepository memberRepository, IMapper<Member, MemberDTO> memberMapper, IMapper<Member, MemberRegistrationDTO> memberRegistrationMapper, ILogger<MemberService> logger)
    {
        _memberRepository = memberRepository;
        _memberMapper = memberMapper;
        _memberRegistrationMapper = memberRegistrationMapper;
        _logger = logger;
    }

    public async Task<ICollection<MemberDTO>> GetAllAsync(int page, int pageSize)
    {
        _logger.LogDebug("Getting members");

        var res = await _memberRepository.GetAllAsync(page, pageSize);
        var dtos = res.Select(user => _memberMapper.MapToDTO(user)).ToList();
        return dtos;                
    }

    public async Task<MemberDTO?> GetByIdAsync(int id)
    {
        _logger.LogDebug("Getting member: {id}", id);

        var res = await _memberRepository.GetByIdAsync(id);
        return res != null ? _memberMapper.MapToDTO(res) : null;
    }

    public async Task<MemberDTO?> GetByNameAsync(string userName)
    {
        _logger.LogDebug("Getting member by username");

        var res = await _memberRepository.GetByNameAsync(userName);
        return res != null ? _memberMapper.MapToDTO(res) : null;
    }  

    public async Task<int> GetAuthenticatedIdAsync(string userName, string password)
    {
        _logger?.LogDebug("Getting authenticated Id");

        var member = await _memberRepository.GetByNameAsync(userName);
        if (member == null)
            return 0;

        if (BCrypt.Net.BCrypt.Verify(password, member.HashedPassword))
        {
            return member.Id;
        }
        return 0;
    }

    public async Task<MemberDTO?> RegisterAsync(MemberRegistrationDTO dto)
    {
        _logger?.LogDebug("Registering new member");

        var existingMember = await _memberRepository.GetByNameAsync(dto.UserName);
        if (existingMember != null)
        {
            return null;           
        }

        var member = _memberRegistrationMapper.MapToEntity(dto);

        member.Salt = BCrypt.Net.BCrypt.GenerateSalt();
        member.HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password, member.Salt);

        var res = await _memberRepository.AddAsync(member);

        return _memberMapper.MapToDTO(res!);
    }

    public async Task<MemberDTO?> UpdateAsync(int id, MemberDTO dto, int loggedInUserId)
    {
        _logger.LogDebug("Updating member: {id}", id);

        var loginUser = await _memberRepository.GetByIdAsync(loggedInUserId);
        if (loginUser == null) return null;

        if (id != loggedInUserId && !loginUser.IsAdminUser)
        {
            throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to update {id}");
        }

        var member = _memberMapper.MapToEntity(dto);
        member.Id = id;

        var res = await _memberRepository.UpdateAsync(id, member);
        return res != null ? _memberMapper.MapToDTO(member) : null;
    } 

    public async Task<MemberDTO?> DeleteByIdAsync(int id, int loggedInUserId)
    {
        _logger.LogDebug("Deleting member: {id}", id);

        var loginUser = await _memberRepository.GetByIdAsync(loggedInUserId);
        if (loginUser == null) return null;

        if (id != loggedInUserId && !loginUser.IsAdminUser)
        {
            throw new UnauthorizedAccessException($"User {loggedInUserId} has no access to delete user {id}");
        }

        var res = await _memberRepository.DeleteByIdAsync(id);
        return res != null ? _memberMapper.MapToDTO(res) : null;
    }

    public async Task<MemberDTO?> AddAsync(MemberDTO dto, int loggedInUserId) // Not used. RegisterAsync() used instead.
    {
        await Task.Delay(10);
        throw new NotImplementedException();
    } 
}
