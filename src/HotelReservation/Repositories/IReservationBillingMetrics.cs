namespace HotelReservation.Repositories;

public interface IReservationBillingMetrics
{
    decimal GetTotalRevenue(DateTime from, DateTime to);
}
