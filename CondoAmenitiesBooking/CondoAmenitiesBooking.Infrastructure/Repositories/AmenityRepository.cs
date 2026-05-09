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
            return await _context.Amenities.FirstOrDefaultAsync(a => a.AmenityId == amenityId);
        }

        public async Task<AmenityUnit?> GetUnitById(int unitId)
        {
            return await _context.AmenityUnits.FirstOrDefaultAsync(x => x.UnitId == unitId);
        }

        public async Task<AmenityTimeSlot?> GetSlotById(int slotId)
        {
            return await _context.AmenityTimeSlots.FirstOrDefaultAsync(x => x.SlotId == slotId);
        }
    }
}
