namespace HotelReservation.Services;

using HotelReservation.Domain;
using HotelReservation.Models;
using HotelReservation.Repositories;

/// <summary>Service applicatif : orchestration (dépôt, domaine, journalisation).</summary>
public class ReservationService
{
    private readonly IReservationDataAccess _reservations;
    private readonly IRoomRepository _rooms;
    private readonly ILogger _logger;
    private readonly ReservationDomainService _domain;
    private int _counter;

    public ReservationService(
        IReservationDataAccess reservations,
        IRoomRepository rooms,
        ILogger logger,
        ReservationDomainService domain)
    {
        _reservations = reservations;
        _rooms = rooms;
        _logger = logger;
        _domain = domain;
    }

    public string CreateReservation(string guestName, string roomId, DateTime checkIn,
        DateTime checkOut, int guestCount, string roomType, string email)
    {
        _logger.Log($"Creating reservation for {guestName}...");

        var room = _rooms.GetById(roomId);
        _domain.EnsureRoomExists(room, roomId);
        _domain.EnsureGuestCapacity(room!, guestCount);

        var existing = _reservations.GetAll();
        _domain.EnsureRoomAvailable(roomId, checkIn, checkOut, existing);

        var total = _domain.ComputeStayTotalPrice(room!, checkIn, checkOut);

        _counter++;
        var reservation = new Reservation
        {
            Id = $"R-{_counter:D3}",
            GuestName = guestName,
            RoomId = roomId,
            CheckIn = checkIn,
            CheckOut = checkOut,
            GuestCount = guestCount,
            RoomType = roomType,
            Status = "Confirmed",
            Email = email,
            TotalPrice = total
        };
        _reservations.Add(reservation);

        _logger.Log($"Reservation {reservation.Id} created.");

        return reservation.Id;
    }

    public Reservation? GetReservation(string id)
    {
        return _reservations.GetById(id);
    }

    public List<Reservation> GetAllReservations()
    {
        return _reservations.GetAll();
    }
}
