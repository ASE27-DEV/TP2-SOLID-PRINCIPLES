namespace HotelReservation.Services;

using HotelReservation.Models;

public class CheckInService
{
    private readonly Dictionary<string, CacheEntry> _cache = new();

    public void ProcessCheckIn(Reservation reservation)
    {
        EnsureConfirmedForCheckIn(reservation);
        RefreshCheckInCache(reservation.Id);
        ApplyLateCheckInFeeIfApplicable(reservation);
        MarkAsCheckedIn(reservation);
        NotifyRoomOccupied(reservation);
    }

    public void ProcessCheckOut(Reservation reservation)
    {
        EnsureCheckedInForCheckOut(reservation);
        reservation.Status = "CheckedOut";
        InvalidateCacheForReservation(reservation.Id);
        NotifyRoomFreed(reservation);
    }

    private static void EnsureConfirmedForCheckIn(Reservation reservation)
    {
        if (reservation.Status != "Confirmed")
            throw new Exception($"Cannot check in: reservation is {reservation.Status}");
    }

    private static void EnsureCheckedInForCheckOut(Reservation reservation)
    {
        if (reservation.Status != "CheckedIn")
            throw new Exception($"Cannot check out: reservation is {reservation.Status}");
    }

    private void RefreshCheckInCache(string reservationId)
    {
        _cache.Remove(reservationId);
        _cache[reservationId] = new CacheEntry(DateTime.Now, "CheckedIn");
    }

    private void InvalidateCacheForReservation(string reservationId)
    {
        _cache.Remove(reservationId);
    }

    private static void ApplyLateCheckInFeeIfApplicable(Reservation reservation)
    {
        var lateCheckInFee = 25m;
        if (DateTime.Now.Hour >= 22)
            reservation.TotalPrice += lateCheckInFee;
    }

    private static void MarkAsCheckedIn(Reservation reservation)
    {
        reservation.Status = "CheckedIn";
    }

    private static void NotifyRoomOccupied(Reservation reservation)
    {
        Console.WriteLine($"[SMS] Room {reservation.RoomId} is now occupied");
    }

    private static void NotifyRoomFreed(Reservation reservation)
    {
        Console.WriteLine($"[SMS] Room {reservation.RoomId} is now free");
    }
}
