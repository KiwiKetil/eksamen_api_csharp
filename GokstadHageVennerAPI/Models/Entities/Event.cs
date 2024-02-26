using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GokstadHageVennerAPI.Models.Entities;

public class Event
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("MemberId")]
    public int MemberId { get; set; }
    [Required]
    [MaxLength(30)]
    public string EventType { get; set; } = string.Empty;
    [Required]
    [MaxLength(30)]
    public string EventName { get; set; } = string.Empty;
    [Required]
    [MinLength(1), MaxLength(200)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateOnly EventDate { get; set; }
    [Required]
    public TimeOnly EventTime { get; set; }
    [Required]
    public DateTime Created { get; set; }
    [Required]
    public DateTime Updated { get; set; }

    public virtual Member? Member { get; set; }
    public virtual ICollection<EventRegistration> EventRegistration { get; set; } = new HashSet<EventRegistration>();
}
