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
            var userId = $"{dto.Block}-{dto.Floor:D2}-{dto.UnitNumber:D2}";

            var exists = await _context.Users.AnyAsync(u => u.UserId == userId);
            if (exists)
                throw new Exception("User already exists");

            //var random = new Random();
            //var randomNumber = random.Next(1000, 9999);
            //var userId = $"{dto.Block}-{dto.Floor:D2}-{dto.Unit:D2}-{randomNumber}";

            var user = new User
            {
                UserId = userId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Mobile = dto.Mobile,
                //PasswordHash = PasswordHasher.Hash(dto.Password),
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                Estate = dto.EstateName,
                Block = dto.Block,
                Floor = dto.Floor,
                Unit = dto.UnitNumber,
                PostalCode = dto.PostalCode,
                OwnerType = dto.UserType
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
                .Where(x => !x.IsDeleted)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    FullName = u.FirstName + " " + u.LastName,
                    Email = u.Email,
                    Mobile = u.Mobile,
                    Block = u.Block,
                    Floor = u.Floor,
                    Unit = u.Unit,
                    IsActive = u.IsActive,
                    IsApproved = u.IsApproved,
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) return false;

            user.IsActive = false;
            user.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RejectUser(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) return false;

            user.IsActive = false;
            user.IsApproved = false;
            user.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ApprovedUser(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null) return false;

            user.IsActive = true;
            user.IsApproved = true;
            user.IsDeleted = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetById(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId && !u.IsDeleted);
        }

        public async Task<(User?, string?)> ValidateUser(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email && !x.IsDeleted);

            if (user == null)
                return (null, "User Information not found");

            if (!user.IsApproved) 
                return (null, "Management not approved your registration request.");

            if (!user.IsActive)
                return (null, "You are inactive");

            var isValid = PasswordHelper.VerifyPassword(user.PasswordHash, dto.Password);

            if (!isValid)
                return (null,"Password not valid");

            return (user, null);
        }
    }
}
