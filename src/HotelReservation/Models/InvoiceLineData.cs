namespace HotelReservation.Models;

/// <summary>Données minimales pour facturation (ISP : InvoiceGenerator ne dépend pas de Reservation entière).</summary>
public record InvoiceLineData(
    string Id,
    string GuestName,
    string RoomId,
    DateTime CheckIn,
    DateTime CheckOut,
    int GuestCount,
    string RoomType)
{
    public static InvoiceLineData FromReservation(Reservation r) =>
        new(r.Id, r.GuestName, r.RoomId, r.CheckIn, r.CheckOut, r.GuestCount, r.RoomType);
}
