namespace HotelReservation.Housekeeping.Domain;

using HotelReservation.Models;

public interface ICleaningNotifier
{
    void NotifyCleaningTask(CleaningTask task);
}
