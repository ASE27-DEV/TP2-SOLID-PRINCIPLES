namespace HotelReservation.Services;

using HotelReservation.Cancellation;
using HotelReservation.Models;

public class CancellationService
{
    private readonly CancellationPolicyRegistry _policyRegistry;

    public CancellationService(CancellationPolicyRegistry policyRegistry)
    {
        _policyRegistry = policyRegistry;
    }

    public decimal CalculateRefund(Reservation reservation, DateTime now)
    {
        var policy = _policyRegistry.Get(reservation.CancellationPolicy);
        return policy.CalculateRefund(reservation, now);
    }

    public void CancelReservation(Reservation reservation, DateTime now)
    {
        var refund = CalculateRefund(reservation, now);
        reservation.Cancel();
        Console.WriteLine(
            $"[OK] Reservation {reservation.Id} cancelled " +
            $"({reservation.CancellationPolicy} policy: " +
            $"{(refund == reservation.TotalPrice ? "full" : "partial")} refund of {refund:F2} EUR)");
    }
}
