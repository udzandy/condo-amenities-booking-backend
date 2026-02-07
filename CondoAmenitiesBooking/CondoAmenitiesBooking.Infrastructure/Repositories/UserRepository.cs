using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
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
    }
}
