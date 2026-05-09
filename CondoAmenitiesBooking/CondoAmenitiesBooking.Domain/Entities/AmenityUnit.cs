namespace CondoAmenitiesBooking.Domain.Entities
{
    public class AmenityUnit
    {
        public int UnitId { get; set; }
        public int AmenityId { get; set; }
        public string UnitName { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        // Navigation
        public Amenity Amenity { get; set; } = default!;
        public ICollection<AmenityTimeSlot> TimeSlots { get; set; } = new List<AmenityTimeSlot>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
