namespace CondoAmenitiesBooking.Domain.Entities
{
    public class AmenityRule
    {
        public int RuleId { get; set; }
        public int AmenityId { get; set; }
        public int MaxDurationMinutes { get; set; }
        public int TimeSlotIntervalMinutes { get; set; }
        public string CancellationPolicy { get; set; } = default!;
        // Navigation
        public Amenity Amenity { get; set; } = default!;
    }
}
