namespace CondoAmenitiesBooking.Application.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public string AmenityName { get; set; } = default!;
        public string UnitName { get; set; } = default!;
        public DateTime BookingDate { get; set; } = default!;
        public string TimeSlot { get; set; } = default!;
        public string Status { get; set; } = default!;
        public bool CanCancel { get; set; }
    }
}
