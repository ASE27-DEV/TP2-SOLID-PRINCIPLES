namespace HotelReservation.Interfaces;

/// <summary>Contrat commun des réservations (sans annulation obligatoire).</summary>
public interface IReservation
{
    string Id { get; }
    string GuestName { get; }
    string Status { get; }
    decimal TotalPrice { get; }
}
