using System.ComponentModel.DataAnnotations;

namespace app_server.Application.DTOs.Reservations;

public class CreateReservationRequest
{
    [Required]
    public int userId { get; set; }
    [Required]
    public int spaceId { get; set; }
    [Required]
    public DateTime startDate { get; set; }
    [Required]
    public DateTime endDate { get; set; }
}