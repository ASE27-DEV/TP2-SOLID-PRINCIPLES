namespace HotelReservation.Services;

using HotelReservation.Models;

public class InvoiceGenerator
{
    public Invoice Generate(InvoiceLineData line)
    {
        var nights = (line.CheckOut - line.CheckIn).Days;
        var pricePerNight = line.RoomType switch
        {
            "Standard" => 80m,
            "Suite" => 200m,
            "Family" => 120m,
            _ => 0m
        };
        var subtotal = nights * pricePerNight;
        var tva = subtotal * 0.10m;
        var touristTax = line.GuestCount * nights * 1.50m;
        var total = subtotal + tva + touristTax;

        return new Invoice
        {
            ReservationId = line.Id,
            GuestName = line.GuestName,
            RoomDescription = $"{line.RoomType} {line.RoomId}",
            Nights = nights,
            Subtotal = subtotal,
            Tva = tva,
            TouristTax = touristTax,
            Total = total
        };
    }

    public void PrintInvoice(Invoice invoice, InvoiceLineData line)
    {
        Console.WriteLine($"Invoice for {invoice.GuestName}:");
        Console.WriteLine($"  Room: {invoice.RoomDescription}, " +
            $"{line.CheckIn:dd/MM} -> {line.CheckOut:dd/MM} " +
            $"({invoice.Nights} nights)");
        Console.WriteLine($"  Subtotal: {invoice.Subtotal:F2} EUR");
        Console.WriteLine($"  TVA (10%): {invoice.Tva:F2} EUR");
        Console.WriteLine($"  Tourist Tax ({line.GuestCount} guests x " +
            $"{invoice.Nights} nights x 1.50 EUR): {invoice.TouristTax:F2} EUR");
        Console.WriteLine($"  Total: {invoice.Total:F2} EUR");
    }
}
