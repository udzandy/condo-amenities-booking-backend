using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

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

        public async Task<List<AmenityDto>> GetAmenities()
        {
            return await _context.Amenities
                .Select(x => new AmenityDto
                {
                    AmenityId = x.AmenityId,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    IsActive = x.IsActive,
                    ImagePath = x.ImagePath,
                    RoutePath = x.RoutePath

                })
                .ToListAsync();
        }

        public async Task<AmenityDto> CreateAmenity(AmenityDto amenity)
        {
            Amenity entity = new Amenity
            {
                Name = amenity.Name,
                Description = amenity.Description,
                Price = amenity.Price,
                ImagePath = amenity.ImagePath,
                RoutePath = amenity.RoutePath,
                IsActive = amenity.IsActive,
            };

            _context.Amenities.Add(entity);

            await _context.SaveChangesAsync();

            return amenity;
        }

        public async Task<AmenityDto?> UpdateAmenity(int id, AmenityDto amenity)
        {
            var entity = await _context.Amenities.FirstOrDefaultAsync(x => x.AmenityId == id);

            if (entity == null)
                return null;

            entity.Name = amenity.Name;
            entity.Description = amenity.Description;
            entity.Price = amenity.Price;
            entity.ImagePath = amenity.ImagePath;
            entity.RoutePath = amenity.RoutePath;
            entity.IsActive = amenity.IsActive;

            await _context.SaveChangesAsync();

            return amenity;
        }

        public async Task DeleteAmenity(int amenityId)
        {
            var amenity = await _context.Amenities.FindAsync(amenityId);

            if (amenity == null)
                return;

            _context.Amenities.Remove(amenity);

            await _context.SaveChangesAsync();
        }

        // =========================
        // UNITS
        // =========================

        public async Task<List<AmenityUnitDto>> GetUnits()
        {
            return await _context.AmenityUnits
                .Include(x => x.Amenity)
                .Select(x => new AmenityUnitDto
                {
                    UnitId = x.UnitId,
                    AmenityId = x.AmenityId,
                    AmenityName = x.Amenity.Name,
                    Name = x.UnitName,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<AddAmenityUnitDto> CreateUnit(AddAmenityUnitDto unit)
        {
            AmenityUnit entity = new AmenityUnit
            {
                UnitName = unit.Name,
                AmenityId = unit.AmenityId,
                IsActive = unit.IsActive,
            };

            _context.AmenityUnits.Add(entity);

            await _context.SaveChangesAsync();

            return unit;
        }

        public async Task<UpdateAmenityUnitDto?> UpdateUnit(int id, UpdateAmenityUnitDto unit)
        {
            var entity = await _context.AmenityUnits
            .FirstOrDefaultAsync(x => x.UnitId == id);

            if (entity == null)
                return null;

            entity.UnitName = unit.Name;
            entity.AmenityId = unit.AmenityId;
            entity.IsActive = unit.IsActive;

            await _context.SaveChangesAsync();

            return unit;
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
        // GET SLOTS
        // =========================

        public async Task<List<AmenitySlotDto>> GetSlots()
        {
            return await _context.AmenityTimeSlots
                .Include(x => x.Unit)
                .Select(x => new AmenitySlotDto
                {
                    SlotId = x.SlotId,
                    UnitId = x.UnitId,
                    UnitName = x.Unit.UnitName,
                    AmenityName = x.Unit.Amenity.Name,
                    AmenityId = x.Unit.Amenity.AmenityId,
                    StartTime = x.StartTime.ToString(@"hh\:mm"),
                    EndTime = x.EndTime.ToString(@"hh\:mm"),
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<AddAmenitySlotDto> CreateSlot(AddAmenitySlotDto slot)
        {
            AmenityTimeSlot entity = new AmenityTimeSlot
            {
                UnitId = slot.UnitId,
                StartTime = TimeSpan.Parse(slot.StartTime),
                EndTime = TimeSpan.Parse(slot.EndTime),
                IsActive = slot.IsActive,
            };

            _context.AmenityTimeSlots.Add(entity);

            await _context.SaveChangesAsync();

            return slot;
        }

        public async Task<UpdateAmenitySlotDto?> UpdateSlot(int id, UpdateAmenitySlotDto slot)
        {
            var entity = await _context.AmenityTimeSlots.FirstOrDefaultAsync(x => x.SlotId == id);

            if (entity == null)
                return null;

            entity.UnitId = slot.UnitId;
            entity.StartTime = TimeSpan.Parse(slot.StartTime);
            entity.EndTime = TimeSpan.Parse(slot.EndTime);
            entity.IsActive = slot.IsActive;

            await _context.SaveChangesAsync();

            return slot;
        }

        public async Task DeleteSlot(int slotId)
        {
            var slot = await _context.AmenityTimeSlots.FindAsync(slotId);

            if (slot == null)
                return;

            _context.AmenityTimeSlots.Remove(slot);

            await _context.SaveChangesAsync();
        }
    }
}
