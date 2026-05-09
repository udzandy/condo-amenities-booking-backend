namespace CondoAmenitiesBooking.Domain.Entities
{
    public class AmenityPolicy
    {
        public int PolicyId { get; set; }
        public int AmenityId { get; set; }
        // Cancellation before X hours
        public int CancellationHours { get; set; }
        // Monthly limit
        public int? MaxBookingsPerMonth { get; set; }
        // Weekly limit
        public int? MaxBookingsPerWeek { get; set; }
        public bool IsActive { get; set; } = true;
        // Navigation
        public Amenity Amenity { get; set; } = default!;
    }
}
