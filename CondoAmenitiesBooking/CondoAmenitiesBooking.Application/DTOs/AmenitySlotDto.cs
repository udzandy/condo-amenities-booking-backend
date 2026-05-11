namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AmenitySlotDto
    {
        public int SlotId { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public string StartTime { get; set; } = default!;
        public string EndTime { get; set; } = default!;
        public bool IsBooked { get; set; }
        public bool IsActive { get; set; }
    }
}
