using CondoAmenitiesBooking.Domain.Enums;

namespace CondoAmenitiesBooking.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string TransactionId { get; set; } = default!;
        public DateTime PaidAt { get; set; }
        // Navigation
        public Booking Booking { get; set; } = default!;
    }
}
