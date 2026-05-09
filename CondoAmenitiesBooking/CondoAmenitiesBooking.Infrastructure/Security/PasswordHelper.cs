using Microsoft.AspNetCore.Identity;

namespace CondoAmenitiesBooking.Infrastructure.Security
{
    public static class PasswordHelper
    {
        private static readonly PasswordHasher<object> hasher = new();

        public static string HashPassword(string password)
        {
            return hasher.HashPassword(null!, password);
        }

        public static bool VerifyPassword(string hashedPassword, string password)
        {
            var result = hasher.VerifyHashedPassword(null!, hashedPassword, password);

            return result == PasswordVerificationResult.Success;
        }
    }
}
