using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app_server.Domain.Entities;

[Table("users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string username { get; set; }
    
    [Required]
    public string password { get; set; }
    
    [Required]
    [EmailAddress]
    public string email { get; set; }
    
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    
    public Collection<Reservation> reservations { get; set; }
}