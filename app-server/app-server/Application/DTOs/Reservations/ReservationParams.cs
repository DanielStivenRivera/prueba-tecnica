namespace app_server.Application.DTOs.Reservations;

public class ReservationParams
{
    public DateTime? startDate { get; set; }
    public DateTime? endDate { get; set; }
    public int? userId { get; set; }
    public int? spaceId { get; set; }
    
    public bool fetchPlaces { get; set; }
}