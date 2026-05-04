namespace CondoAmenitiesBooking.Domain.Entities
{
    public class AuditLog
    {
        public int LogId { get; set; }
        public string UserId { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string Entity { get; set; } = default!;
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        // Navigation
        public User User { get; set; } = default!;
    }
}
