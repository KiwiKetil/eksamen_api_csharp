using System.ComponentModel.DataAnnotations;

namespace GokstadHageVennerAPI.Models.Entities;

public class Member
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(3), MaxLength(30)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [MinLength(1), MaxLength(30)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MinLength(1), MaxLength(30)]
    public string LastName { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;
    [Required]
    public DateTime Created { get; set; }
    [Required]
    public DateTime Updated { get; set; }   
    [Required]
    public bool IsAdminUser { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new HashSet<Event>();
    public virtual ICollection<EventRegistration> EventRegistration { get; set; } = new HashSet<EventRegistration>();
}
