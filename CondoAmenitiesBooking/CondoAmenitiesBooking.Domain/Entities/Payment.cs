using CondoAmenitiesBooking.Domain.Enums;

namespace CondoAmenitiesBooking.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod? PaymentMethod { get; set; } // Card / PayNow / Stripe
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending; // Pending, Paid, Refunded, Failed, Cancelled
        public Guid? TransactionId { get; set; } = default!;
        public DateTime? PaidAt { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Booking Booking { get; set; } = default!;
    }
}
