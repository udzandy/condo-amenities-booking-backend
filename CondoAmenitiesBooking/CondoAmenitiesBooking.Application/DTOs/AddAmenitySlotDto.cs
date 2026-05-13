namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AddAmenitySlotDto
    {
        public int SlotId { get; set; }
        public int UnitId { get; set; }
        public int AmenityId { get; set; }
        public string StartTime { get; set; } = default!;
        public string EndTime { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
