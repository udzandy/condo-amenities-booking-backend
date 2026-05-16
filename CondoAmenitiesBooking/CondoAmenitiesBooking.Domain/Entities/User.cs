using CondoAmenitiesBooking.Domain.Enums;

namespace CondoAmenitiesBooking.Domain.Entities
{
    public class User
    {
        public string UserId { get; set; } = default!; // 61-01-01
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Estate { get; set; } = default!;
        public int Block { get; set; }
        public int Floor { get; set; }
        public int Unit { get; set; }
        public string PostalCode { get; set; } = default!;
        public OwnerType OwnerType { get; set; } = OwnerType.Owner;
        public UserRole Role { get; set; } = UserRole.Resident;
        public bool IsActive { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
