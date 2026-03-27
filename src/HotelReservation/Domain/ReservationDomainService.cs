namespace HotelReservation.Domain;

using HotelReservation.Models;

/// <summary>Règles métier : disponibilité, capacité, prix de base (sans orchestration ni persistance).</summary>
public class ReservationDomainService
{
    public void EnsureRoomExists(Room? room, string roomId)
    {
        if (room == null)
            throw new Exception($"Room {roomId} not found");
    }

    public void EnsureGuestCapacity(Room room, int guestCount)
    {
        if (guestCount > room.MaxGuests)
            throw new Exception($"Room {room.Id} max capacity is {room.MaxGuests}");
    }

    public void EnsureRoomAvailable(
        string roomId,
        DateTime checkIn,
        DateTime checkOut,
        IEnumerable<Reservation> existingReservations)
    {
        var conflict = existingReservations.Any(r =>
            r.RoomId == roomId &&
            r.Status != "Cancelled" &&
            r.CheckIn < checkOut &&
            r.CheckOut > checkIn);

        if (conflict)
            throw new Exception($"Room {roomId} is not available for {checkIn:dd/MM} -> {checkOut:dd/MM}");
    }

    public decimal ComputeStayTotalPrice(Room room, DateTime checkIn, DateTime checkOut)
    {
        var nights = (checkOut - checkIn).Days;
        return nights * room.PricePerNight;
    }
}
