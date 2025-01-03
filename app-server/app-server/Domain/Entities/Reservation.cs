using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app_server.Domain.Entities;

[Table("reservations")]
public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    [Required]
    public int userId { get; set; }
    [Required]
    public int spaceId { get; set; }
    [Required]
    public DateTime startDate { get; set; }
    [Required]
    public DateTime endDate { get; set; }
    
    public virtual User user { get; set; }
    
    public virtual Space space { get; set; }
}