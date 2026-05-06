namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AuditLogFilterDto
    {
        public string? UserId { get; set; }
        public string? Action { get; set; }
        public string? Entity { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? SearchText { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
