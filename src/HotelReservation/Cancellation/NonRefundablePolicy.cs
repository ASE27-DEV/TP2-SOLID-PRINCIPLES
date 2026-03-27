namespace HotelReservation.Cancellation;

using HotelReservation.Models;

public class NonRefundablePolicy : ICancellationPolicy
{
    public string PolicyName => "NonRefundable";

    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        return 0m;
    }
}
