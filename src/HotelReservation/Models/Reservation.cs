namespace HotelReservation.Models;

/// <summary>
/// Données et cycle de vie (réceptionniste : annulation / statut).
/// Tarification et planning ménage : voir BillingCalculator et HousekeepingScheduler.
/// </summary>
public class Reservation
{
    public string Id { get; set; } = string.Empty;
    public string GuestName { get; set; } = string.Empty;
    public string RoomId { get; set; } = string.Empty;
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int GuestCount { get; set; }
    public string RoomType { get; set; } = string.Empty;
    public string Status { get; set; } = "Confirmed";
    public string CancellationPolicy { get; set; } = "Flexible";
    public string Email { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }

    /// <summary>Acteur : réceptionniste — règles d'annulation (avant séjour).</summary>
    public void Cancel()
    {
        if (Status == "CheckedIn")
            throw new InvalidOperationException("Cannot cancel after check-in");
        Status = "Cancelled";
    }
}
