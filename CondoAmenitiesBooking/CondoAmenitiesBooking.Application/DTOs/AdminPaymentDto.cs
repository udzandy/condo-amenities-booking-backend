namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AdminPaymentDto
    {
        public int BookingId { get; set; }
        public string UserName { get; set; }
        public string AmenityName { get; set; }
        public string UnitName { get; set; }
        public string SlotTime { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsPaid { get; set; }
        public string BookingStatus { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
