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
        public string Estate { get; set; } = default!;
        public int Block { get; set; } = default!;
        public int Floor { get; set; }
        public int Unit { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public OwnerType OwnerType { get; set; } = OwnerType.Owner;
        //public UserRole Role { get; set; } = UserRole.User;
    }
}
