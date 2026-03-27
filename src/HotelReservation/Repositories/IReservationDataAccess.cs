namespace HotelReservation.Repositories;

using HotelReservation.Models;

/// <summary>Persistance et requêtes courantes (ISP : métriques séparées).</summary>
public interface IReservationDataAccess
{
    Reservation? GetById(string id);
    List<Reservation> GetAll();
    List<Reservation> GetByDateRange(DateTime from, DateTime to);
    List<Reservation> GetByGuest(string guestName);
    void Add(Reservation reservation);
    void Update(Reservation reservation);
    void Delete(string id);
}
