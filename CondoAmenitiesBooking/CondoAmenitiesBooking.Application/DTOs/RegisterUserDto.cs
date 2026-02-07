using CondoAmenitiesBooking.Domain.Enums;

namespace CondoAmenitiesBooking.Application.DTOs
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string EstateName { get; set; } = default!;
        public string Block { get; set; } = default!;
        public string UnitNumber { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public OccupancyType OccupancyType { get; set; } = OccupancyType.Tenant;
        public UserRole Role { get; set; } = UserRole.User;
    }
}
