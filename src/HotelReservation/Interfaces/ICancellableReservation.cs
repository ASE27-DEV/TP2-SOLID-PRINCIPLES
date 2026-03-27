namespace HotelReservation.Interfaces;

/// <summary>Réservations pouvant être annulées avec remboursement (LSP : pas d'appel invalide).</summary>
public interface ICancellableReservation : IReservation
{
    void Cancel();
    decimal CalculateRefund();
}
