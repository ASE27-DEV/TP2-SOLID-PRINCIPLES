namespace HotelReservation.Services;

/// <summary>Abstraction de journalisation (DIP : consommateur BookingService).</summary>
public interface ILogger
{
    void Log(string message);
}
