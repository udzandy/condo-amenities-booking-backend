using Microsoft.AspNetCore.Identity;

namespace CondoAmenitiesBooking.Infrastructure.Services
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string password);
    }

    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var result = _hasher.VerifyHashedPassword(null!, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
