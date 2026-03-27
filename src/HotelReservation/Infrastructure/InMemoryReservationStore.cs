namespace HotelReservation.Infrastructure;

using HotelReservation.Models;
using HotelReservation.Services;

public class InMemoryReservationStore : IReservationRepository
{
    private readonly Dictionary<string, Reservation> _reservations = new();

    public void Add(Reservation reservation)
    {
        _reservations[reservation.Id] = reservation;
    }

    public Reservation? GetById(string id)
    {
        return _reservations.TryGetValue(id, out var reservation) ? reservation : null;
    }
}
