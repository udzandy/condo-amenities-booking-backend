using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Infrastructure.Persistence;
using CondoAmenitiesBooking.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, IUserService
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<string> Register(RegisterUserDto dto)
        {
            var userId = $"{dto.Block}-{dto.Floor:D2}-{dto.Unit:D2}";

            var exists = await _context.Users.AnyAsync(u => u.UserId == userId);
            if (exists)
                throw new Exception("User already exists");

            var user = new User
            {
                UserId = userId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Mobile = dto.Mobile,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Estate = dto.Estate,
                Block = dto.Block,
                Floor = dto.Floor,
                Unit = dto.Unit,
                PostalCode = dto.PostalCode,
                OwnerType = dto.OwnerType
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return userId;
        }

        public async Task<(string?, string?)> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.IsActive);

            if (user == null)
                return (null, string.Empty);

            if (!PasswordHasher.Verify(dto.Password, user.PasswordHash))
                return (null, String.Empty);

            var name = user.FirstName + " " + user.LastName;

            return (user.UserId, name); // later replace with JWT
        }

        public async Task<List<UserDto>> GetAllActiveUsers()
        {
            return await _context.Users
                .Where(x => x.IsActive)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Name = u.FirstName + " " + u.LastName,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsActive = false; // soft delete
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetById(string userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
