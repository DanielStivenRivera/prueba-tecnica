using app_server.Application.DTOs.Reservations;
using app_server.Application.services;
using app_server.Domain.Entities;

namespace app_server.Adapters;

public class ReservationAdapter
{
    private readonly ReservationService _reservationService;
    
    public ReservationAdapter() {}
    
    public ReservationAdapter(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }
    
    public virtual Reservation CreateReservation(CreateReservationRequest createReservationRequest)
    {
        var reservation = new Reservation
        {
            userId = createReservationRequest.userId,
            spaceId = createReservationRequest.spaceId,
            startDate = createReservationRequest.startDate,
            endDate = createReservationRequest.endDate
        };
        return _reservationService.CreateReservation(reservation);
    }
    
    public virtual void DeleteReservation(int id)
    {
        _reservationService.DeleteReservation(id);
    }
    
    public virtual IEnumerable<Reservation> GetReservations(ReservationParams reservationParams)
    {
        return _reservationService.GetReservations(reservationParams.startDate, reservationParams.endDate, reservationParams.userId, reservationParams.spaceId);
    }
    
}