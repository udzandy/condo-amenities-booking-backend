using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Repositories
{
    public class AmenityRepository : IAmenityService
    {
        private readonly AppDbContext _context;

        public AmenityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Amenity?> GetById(int amenityId)
        {
            return await _context.Amenities
                .FirstOrDefaultAsync(a => a.AmenityId == amenityId);
        }
    }
}
