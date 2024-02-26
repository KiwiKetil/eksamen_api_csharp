namespace GokstadHageVennerAPI.Models.DTOs;

public record MemberDTO(

    int Id,
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    DateTime Created,
    DateTime Updated);
