using CondoAmenitiesBooking.Domain.Enums;

namespace CondoAmenitiesBooking.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string UserId { get; set; } = default!;
        public int AmenityId { get; set; }
        //public string UnitName { get; set; } = default!;
        public int UnitId { get; set; }
        public int SlotId { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Confirmed;
        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Concurrency (IMPORTANT for double booking prevention)
        public byte[] RowVersion { get; set; } = default!;
        // Navigation
        public User User { get; set; } = default!;
        public Amenity Amenity { get; set; } = default!;
        public AmenityUnit Unit { get; set; } = default!;
        public AmenityTimeSlot Slot { get; set; } = default!;
        //public Payment? Payment { get; set; }
    }
}
