namespace CondoAmenitiesBooking.Application.Features.Bookings.Commands
{
    public class CancelBookingCommand
    {
        public int BookingId { get; set; }
        public string UserId { get; set; } = default!;
    }
}
