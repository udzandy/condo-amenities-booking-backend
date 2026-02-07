namespace CondoAmenitiesBooking.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string EstateName { get; set; } = default!;
        public string Block { get; set; } = default!;
        public string UnitNumber { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string OccupancyType { get; set; } = default!; // Owner/Tenant
    }
}
