namespace CondoAmenitiesBooking.Application.DTOs
{
    public class PayBookingRequestDto
    {
        public int BookingId { get; set; }
        public int PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
}
