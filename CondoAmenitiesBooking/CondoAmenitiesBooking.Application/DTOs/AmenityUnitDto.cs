namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AmenityUnitDto
    {
        public int UnitId { get; set; }
        public string Name { get; set; }
        public List<TimeSlotDto> Slots { get; set; }
    }
}
