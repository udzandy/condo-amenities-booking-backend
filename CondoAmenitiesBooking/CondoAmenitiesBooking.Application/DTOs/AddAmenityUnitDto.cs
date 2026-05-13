namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AddAmenityUnitDto
    {
        public int UnitId { get; set; }
        public int AmenityId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
