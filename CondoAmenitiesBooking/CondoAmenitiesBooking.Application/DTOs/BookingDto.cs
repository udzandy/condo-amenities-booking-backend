namespace CondoAmenitiesBooking.Application.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public string AmenityName { get; set; } = default!;
        public DateTime Date { get; set; }
        public string TimeRange { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
