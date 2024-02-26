namespace GokstadHageVennerAPI.Models.DTOs;

public class EventDTO
{  
    public int Id { get; init; }
    public int MemberId { get; set; }
    public string EventType { get; init; } = string.Empty;
    public string EventName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateOnly EventDate { get; set; }
    public TimeOnly EventTime { get; set; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}
