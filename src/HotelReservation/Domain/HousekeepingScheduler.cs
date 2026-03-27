namespace HotelReservation.Domain;

using HotelReservation.Models;

/// <summary>Responsabilité : acteur gouvernante — planning des changements de linge.</summary>
public class HousekeepingScheduler
{
    public List<DateTime> GetLinenChangeDays(Reservation reservation)
    {
        var days = new List<DateTime>();
        var current = reservation.CheckIn.AddDays(3);
        while (current < reservation.CheckOut)
        {
            days.Add(current);
            current = current.AddDays(3);
        }
        return days;
    }
}
