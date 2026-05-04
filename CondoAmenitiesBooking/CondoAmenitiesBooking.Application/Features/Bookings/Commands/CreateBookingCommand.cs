namespace CondoAmenitiesBooking.Application.Features.Bookings.Commands
{
    public class CreateBookingCommand
    {
        public string UserId { get; set; } = default!;
        public int AmenityId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
