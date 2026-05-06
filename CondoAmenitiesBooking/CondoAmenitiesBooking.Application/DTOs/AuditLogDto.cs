namespace CondoAmenitiesBooking.Application.DTOs
{
    public class AuditLogDto
    {
        public int LogId { get; set; }
        public string UserName { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string Entity { get; set; } = default!;
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
