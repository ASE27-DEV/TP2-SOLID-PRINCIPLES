namespace HotelReservation.Infrastructure;

using HotelReservation.Housekeeping.Domain;
using HotelReservation.Models;

public class EmailCleaningNotifier : ICleaningNotifier
{
    private readonly EmailSender _emailSender = new();

    public void NotifyCleaningTask(CleaningTask task)
    {
        _emailSender.Send(
            task.HousekeeperEmail,
            "New cleaning task",
            $"Room {task.RoomId} needs {task.Type} on {task.Date:dd/MM/yyyy}");
    }
}
