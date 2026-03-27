namespace HotelReservation.Services;

using HotelReservation.Domain;
using HotelReservation.Housekeeping.Domain;
using HotelReservation.Models;

public class HousekeepingService
{
    private readonly ICleaningNotifier _notifier;
    private readonly HousekeepingScheduler _scheduler = new();

    public HousekeepingService(ICleaningNotifier notifier)
    {
        _notifier = notifier;
    }

    public List<CleaningTask> GenerateLinenChangeSchedule(Reservation reservation)
    {
        var tasks = new List<CleaningTask>();
        foreach (var date in _scheduler.GetLinenChangeDays(reservation))
        {
            tasks.Add(new CleaningTask
            {
                RoomId = reservation.RoomId,
                Date = date,
                Type = "LinenChange",
                HousekeeperEmail = "housekeeping@masdesoliviers.fr",
                Time = new TimeSpan(10, 0, 0)
            });
        }
        return tasks;
    }

    public void NotifyHousekeeper(CleaningTask task)
    {
        _notifier.NotifyCleaningTask(task);
    }
}
