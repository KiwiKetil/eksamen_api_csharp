using GokstadHageVennerAPI.Models.DTOs;
using GokstadHageVennerAPI.Models.Entities;
using StudentBloggAPI.Mappers.Interfaces;

namespace GokstadHageVennerAPI.Mappers;

public class MemberMapper : IMapper<Member, MemberDTO>
{
    public MemberDTO MapToDTO(Member entity)
    {
        return new MemberDTO(entity.Id, entity.UserName, entity.FirstName, entity.LastName, entity.Email, entity.Created, entity.Updated);
    }

    public Member MapToEntity(MemberDTO dto)
    {        
        var dtNow = DateTime.Now;
        return new Member()
        {
            Id = dto.Id,
            UserName = dto.UserName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Created = dtNow,
            Updated = dtNow
        };        
    }
}
