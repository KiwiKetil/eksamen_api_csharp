using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;

namespace GokstadHageVennerAPI.Services.Interfaces;

public interface IMemberService : IService<MemberDTO>
{
    Task<MemberDTO?> GetByNameAsync(string userName);
    Task<MemberDTO?> RegisterAsync(MemberRegistrationDTO dto);
    Task<int> GetAuthenticatedIdAsync(string userName, string password);
}
