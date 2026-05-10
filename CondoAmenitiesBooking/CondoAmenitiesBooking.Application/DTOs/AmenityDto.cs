namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AmenityDto
    {
        public int AmenityId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = default!;
        public string RoutePath { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
