namespace GokstadHageVennerAPI.Models.DTOs;

public class EventRegistrationDTO
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int MemberId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Created { get; init; }
    public DateTime Updated { get; set; }   
}
