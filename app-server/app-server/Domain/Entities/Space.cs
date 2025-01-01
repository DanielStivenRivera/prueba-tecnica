using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app_server.Domain.Entities;

[Table("spaces")]
public class Space
{
    [Key]
    public int id { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public int capacity { get; set; }

    public ICollection<Reservation> reservations { get; set; }
}