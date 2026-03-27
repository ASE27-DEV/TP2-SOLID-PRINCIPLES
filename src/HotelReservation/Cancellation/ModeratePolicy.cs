namespace HotelReservation.Cancellation;

using HotelReservation.Models;

public class ModeratePolicy : ICancellationPolicy
{
    public string PolicyName => "Moderate";

    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        var daysBeforeCheckIn = (reservation.CheckIn - now).Days;
        if (daysBeforeCheckIn >= 5) return reservation.TotalPrice;
        if (daysBeforeCheckIn >= 2) return reservation.TotalPrice * 0.5m;
        return 0m;
    }
}
