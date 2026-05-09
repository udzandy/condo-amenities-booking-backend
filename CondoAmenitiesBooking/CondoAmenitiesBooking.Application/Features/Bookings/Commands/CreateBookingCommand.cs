namespace CondoAmenitiesBooking.Application.Features.Bookings.Commands
{
    public class CreateBookingCommand
    {
        public string UserId { get; set; } = default!;
        public int AmenityId { get; set; }
        public int UnitId { get; set; }
        public int SlotId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
