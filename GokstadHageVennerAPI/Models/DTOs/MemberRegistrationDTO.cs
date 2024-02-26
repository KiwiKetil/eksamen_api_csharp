namespace GokstadHageVennerAPI.Models.DTOs;

public record MemberRegistrationDTO(
    string UserName,
    string FirstName,
    string LastName,
    string Password,
    string Email
    );
