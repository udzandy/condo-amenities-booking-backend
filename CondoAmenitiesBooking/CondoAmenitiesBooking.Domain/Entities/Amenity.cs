namespace CondoAmenitiesBooking.Domain.Entities
{
    public class Amenity
    {
        public int AmenityId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public AmenityPolicy Policy { get; set; } = default!;
        //public int Capacity { get; set; }
        //public bool IsPaid { get; set; }
        //public decimal Price { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //// Navigation
        //public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        //public ICollection<AmenityRule> Rules { get; set; } = new List<AmenityRule>();
        // Navigation
        public ICollection<AmenityUnit> Units { get; set; } = new List<AmenityUnit>();
    }
}
