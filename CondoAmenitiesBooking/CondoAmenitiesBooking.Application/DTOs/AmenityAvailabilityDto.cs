namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AmenityAvailabilityDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; } = default!;
        public List<AmenitySlotDto> Slots { get; set; } = new();
    }
}
