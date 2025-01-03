using app_server.Application.interfaces;
using app_server.Domain.Entities;

namespace app_server.Infrastructure.Persistence.Repositories;

public class ReservationRepository :  IReservationRepository
{

    private readonly ApplicationDbContext _context;
    
    public ReservationRepository() {}

    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public virtual Reservation Save(Reservation reservation)
    {
        var newReservation = _context.Add<Reservation>(reservation);
        _context.SaveChanges();
        return newReservation.Entity;
    }
    
    public virtual Reservation? GetById(int id)
    {
        return _context.Find<Reservation>(id);
    }
    
    public virtual IEnumerable<Reservation> Search(DateTime? startDate, DateTime? endDate, int? userId, int? spaceId, bool? fetchPlaces)
    {
        return _context.Set<Reservation>()
            .Where(r => 
                (!startDate.HasValue || !endDate.HasValue || r.startDate < endDate && r.endDate > startDate) && 
                (!userId.HasValue || r.userId == userId) &&
                (!spaceId.HasValue || r.spaceId == spaceId)
            )
            .ToList();
    }

    
    public virtual void Delete(int id)
{
    var reservation = _context.Find<Reservation>(id);
    if (reservation != null)
    {
        _context.Remove(reservation);
        _context.SaveChanges();
    }
}
}