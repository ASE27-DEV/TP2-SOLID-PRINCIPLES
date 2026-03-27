namespace HotelReservation.Repositories;

public interface IReservationOccupancyMetrics
{
    Dictionary<string, int> GetOccupancyStats(DateTime from, DateTime to);
}
