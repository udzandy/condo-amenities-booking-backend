using CondoAmenitiesBooking.Domain.Enums;

namespace CondoAmenitiesBooking.Domain.Entities
{
    public class User
    {
        //public Guid Id { get; set; }
        //public string FirstName { get; set; } = default!;
        //public string LastName { get; set; } = default!;
        //public string Email { get; set; } = default!;
        //public string Mobile { get; set; } = default!;
        //public string PasswordHash { get; set; } = default!;
        //public string EstateName { get; set; } = default!;
        //public string Block { get; set; } = default!;
        //public string UnitNumber { get; set; } = default!;
        //public string PostalCode { get; set; } = default!;
        //public OccupancyType OccupancyType { get; set; } = OccupancyType.Tenant;
        //public UserRole Role { get; set; } = UserRole.User;

        public string UserId { get; set; } = default!; // 61-01-01 format
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
