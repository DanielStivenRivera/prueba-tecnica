using app_server.Domain.Entities;
using app_server.Infrastructure.Persistence.Repositories;

namespace app_server.Application.services;

public class ReservationService
{
    private readonly ReservationRepository _reservationRepository;
    
    public ReservationService() {}
    
    public ReservationService(ReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }
    
    public virtual Reservation CreateReservation(Reservation reservation)
    {
        var duration = reservation.endDate - reservation.startDate;
        if (duration < TimeSpan.FromHours(2) || duration > TimeSpan.FromHours(5))
        {
            throw new InvalidOperationException("Reservation duration must be between 2 and 5 hours");
        }
        
        var existingUserReservations = _reservationRepository.Search(reservation.startDate, reservation.endDate, reservation.userId, null);
        if (existingUserReservations.Any())
        {
            throw new InvalidOperationException("User cant have more than one reservation at the same time");
        }
        
        var existingSpaceReservations = _reservationRepository.Search(reservation.startDate, reservation.endDate, null, reservation.spaceId);
        
        if (existingSpaceReservations.Any())
        {
            throw new InvalidOperationException("Space already reserved for this time");
        }
        return _reservationRepository.Save(reservation);
    }
    
    public virtual void DeleteReservation(int id)
    {
        var reservation = _reservationRepository.GetById(id);
        if (reservation == null)
        {
            throw new InvalidOperationException("Reservation not found");
        }
        _reservationRepository.Delete(id);
    }
    
    public virtual IEnumerable<Reservation> GetReservations(DateTime? startDate, DateTime? endDate, int? userId, int? spaceId)
    {
        return _reservationRepository.Search(startDate, endDate, userId, spaceId);
    }
    
}