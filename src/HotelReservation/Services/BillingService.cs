namespace HotelReservation.Services;

using HotelReservation.Repositories;

public class BillingService
{
    private readonly IReservationBillingMetrics _metrics;

    public BillingService(IReservationBillingMetrics metrics)
    {
        _metrics = metrics;
    }

    public decimal GetRevenueForPeriod(DateTime from, DateTime to)
    {
        return _metrics.GetTotalRevenue(from, to);
    }
}
