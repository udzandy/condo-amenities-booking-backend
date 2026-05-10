using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Services
{
    public class AmenityAdminService : IAmenityAdminService
    {
        private readonly AppDbContext _context;

        public AmenityAdminService(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // AMENITIES
        // =========================

        public async Task<List<Amenity>> GetAmenities()
        {
            return await _context.Amenities
                .Include(x => x.Units)
                .ThenInclude(x => x.TimeSlots)
                .ToListAsync();
        }

        public async Task<Amenity> CreateAmenity(Amenity amenity)
        {
            _context.Amenities.Add(amenity);

            await _context.SaveChangesAsync();

            return amenity;
        }

        public async Task UpdateAmenity(Amenity amenity)
        {
            _context.Amenities.Update(amenity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAmenity(int amenityId)
        {
            var amenity =
                await _context.Amenities.FindAsync(amenityId);

            if (amenity == null)
                return;

            _context.Amenities.Remove(amenity);

            await _context.SaveChangesAsync();
        }

        // =========================
        // UNITS
        // =========================

        public async Task<AmenityUnit> CreateUnit(AmenityUnit unit)
        {
            _context.AmenityUnits.Add(unit);

            await _context.SaveChangesAsync();

            return unit;
        }

        public async Task UpdateUnit(AmenityUnit unit)
        {
            _context.AmenityUnits.Update(unit);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUnit(int unitId)
        {
            var unit =
                await _context.AmenityUnits.FindAsync(unitId);

            if (unit == null)
                return;

            _context.AmenityUnits.Remove(unit);

            await _context.SaveChangesAsync();
        }

        // =========================
        // SLOTS
        // =========================

        public async Task<AmenityTimeSlot> CreateSlot(
            AmenityTimeSlot slot)
        {
            _context.AmenityTimeSlots.Add(slot);

            await _context.SaveChangesAsync();

            return slot;
        }

        public async Task UpdateSlot(AmenityTimeSlot slot)
        {
            _context.AmenityTimeSlots.Update(slot);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSlot(int slotId)
        {
            var slot =
                await _context.AmenityTimeSlots.FindAsync(slotId);

            if (slot == null)
                return;

            _context.AmenityTimeSlots.Remove(slot);

            await _context.SaveChangesAsync();
        }
    }
}
