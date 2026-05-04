using CondoAmenitiesBooking.Domain.Enums;

namespace CondoAmenitiesBooking.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string UserId { get; set; } = default!;
        public int AmenityId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Confirmed;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        // Concurrency (IMPORTANT for double booking prevention)
        public byte[] RowVersion { get; set; } = default!;
        // Navigation
        public User User { get; set; } = default!;
        public Amenity Amenity { get; set; } = default!;
        public Payment? Payment { get; set; }
    }
}
