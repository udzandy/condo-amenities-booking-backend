namespace CondoAmenitiesBooking.Application.DTOs
{
    public class UserDto
    {
        public string UserId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Mobile { get; set; } = default!;
        public int Block { get; set; }
        public int Floor { get; set; }
        public int Unit { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsApproved { get; set; } = false;
    }
}
