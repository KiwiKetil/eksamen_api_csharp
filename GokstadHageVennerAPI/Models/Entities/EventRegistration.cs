using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GokstadHageVennerAPI.Models.Entities;

public class EventRegistration
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("EventId")]
    public int EventId { get; set; }
    [ForeignKey("MemberId")]
    public int MemberId { get; set; }
    [Required]
    [MinLength(1), MaxLength(100)]
    public string Status { get; set; } = string.Empty;
    [Required]
    public DateTime Created { get; set; }
    [Required]
    public DateTime Updated { get; set; }
   
    public virtual Member? Member { get; set; }
    public virtual Event? Events { get; set; }
}
