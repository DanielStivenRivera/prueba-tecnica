using app_server.Domain.Entities;

namespace app_server.Application.interfaces;

public interface IReservationRepository
{
    Reservation Save(Reservation reservation);
    Reservation? GetById(int id);
    IEnumerable<Reservation> Search(DateTime? startDate, DateTime? endDate, int? userId, int? spaceId, bool? fetchPlaces);
    void Delete(int id);
}