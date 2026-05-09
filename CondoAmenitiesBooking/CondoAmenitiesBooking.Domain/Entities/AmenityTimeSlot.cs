namespace CondoAmenitiesBooking.Domain.Entities
{
    public class AmenityTimeSlot
    {
        public int SlotId { get; set; }
        public int UnitId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; } = true;
        // Navigation
        public AmenityUnit Unit { get; set; } = default!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
