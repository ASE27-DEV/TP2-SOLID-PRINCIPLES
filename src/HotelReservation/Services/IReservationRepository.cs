namespace HotelReservation.Services;

using HotelReservation.Models;

/// <summary>Contrat minimal pour la persistance des réservations (DIP : BookingService).</summary>
public interface IReservationRepository
{
    void Add(Reservation reservation);
    Reservation? GetById(string id);
}
