namespace CondoAmenitiesBooking.Application.DTOs
{
    public class RecentBookingDto
    {
        public int BookingId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string AmenityName { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string TimeSlot { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
