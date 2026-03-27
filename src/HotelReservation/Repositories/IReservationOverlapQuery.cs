namespace HotelReservation.Repositories;

using HotelReservation.Models;

/// <summary>Seule dépendance nécessaire pour savoir quelles chambres sont prises (ISP).</summary>
public interface IReservationOverlapQuery
{
    List<Reservation> GetByDateRange(DateTime from, DateTime to);
}
