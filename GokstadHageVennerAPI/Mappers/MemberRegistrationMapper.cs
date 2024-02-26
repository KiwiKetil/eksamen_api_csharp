using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using StudentBloggAPI.Mappers.Interfaces;

namespace GokstadHageVennerAPI.Mappers;

public class MemberRegistrationMapper : IMapper<Member, MemberRegistrationDTO>
{
    public MemberRegistrationDTO MapToDTO(Member entity)
    {
        throw new NotImplementedException();
    }

    public Member MapToEntity(MemberRegistrationDTO dto)
    {
        var dtNow = DateTime.Now;
        return new Member ()
        {
            Email = dto.Email,
            UserName = dto.UserName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IsAdminUser = false,
            Created = dtNow,
            Updated = dtNow
        };
    }
}
