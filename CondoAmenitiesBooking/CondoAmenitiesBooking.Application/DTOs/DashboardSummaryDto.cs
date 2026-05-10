namespace CondoAmenitiesBooking.Application.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalBookings { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalAmenities { get; set; }
        public int TodayBookings { get; set; }
        public int CancelledBookings { get; set; }
    }
}
