namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AmenityBookingConfigDto
    {
        public int AmenityId { get; set; }
        public string Title { get; set; }
        public string UnitsLabel { get; set; }
        public List<AmenityUnitDto> Units { get; set; }
        public Dictionary<string, List<BookedSlotDto>> BookedSlots { get; set; }
    }
}
