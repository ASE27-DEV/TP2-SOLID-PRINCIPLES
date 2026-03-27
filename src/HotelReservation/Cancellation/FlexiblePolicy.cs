namespace HotelReservation.Cancellation;

using HotelReservation.Models;

public class FlexiblePolicy : ICancellationPolicy
{
    public string PolicyName => "Flexible";

    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        var daysBeforeCheckIn = (reservation.CheckIn - now).Days;
        return daysBeforeCheckIn >= 1 ? reservation.TotalPrice : 0m;
    }
}
