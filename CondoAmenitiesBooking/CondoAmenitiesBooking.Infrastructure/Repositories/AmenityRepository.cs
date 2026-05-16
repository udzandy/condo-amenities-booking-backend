using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Domain.Enums;
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

        public async Task<List<AmenityDto>> GetAmenities()
        {
            return await _context.Amenities
                .Where(x => x.IsActive)
                .Select(x => new AmenityDto
                {
                    AmenityId = x.AmenityId,
                    Name = x.Name,
                    Price = x.Price,
                    ImagePath = x.ImagePath,
                    RoutePath = x.RoutePath
                })
                .ToListAsync();
        }

        public async Task<List<AmenityAvailabilityDto>> GetAvailability(string slug , DateTime bookingDate)
        {
            var amenity = await _context.Amenities.FirstOrDefaultAsync(a => a.RoutePath == slug);

            // GET UNITS + SLOTS
            var units = await _context.AmenityUnits
                .Where(x => x.AmenityId == amenity!.AmenityId)
                .Include(x => x.TimeSlots)
                .ToListAsync();

            // GET BOOKINGS
            var bookings = await _context.Bookings
                .Where(x =>
                    x.AmenityId == amenity!.AmenityId &&
                    x.BookingDate.Date == bookingDate.Date &&
                    x.BookingStatus == BookingStatus.Confirmed)
                .ToListAsync();

            // BUILD RESPONSE
            var result = units.Select(unit =>
            {
                return new AmenityAvailabilityDto
                {
                    UnitId = unit.UnitId,
                    UnitName = unit.UnitName,
                    Slots = unit.TimeSlots.Select(slot =>
                    {
                        var isBooked = bookings.Any(b => b.UnitId == unit.UnitId && b.SlotId == slot.SlotId);

                        return new AmenitySlotDto
                        {
                            SlotId = slot.SlotId,
                            StartTime = DateTime.Today.Add(slot.StartTime).ToString("hh:mm tt"),
                            EndTime = DateTime.Today.Add(slot.EndTime).ToString("hh:mm tt"),
                            IsBooked = isBooked
                        };

                    }).ToList()
                };

            }).ToList();

            return result;
        }

        public async Task<AmenityBookingConfigDto> GetBookingConfig(string slug, DateTime date)
        {
            var amenity = await _context.Amenities.FirstOrDefaultAsync(a => a.RoutePath == slug);

            var units = await _context.AmenityUnits.Where(u => u.AmenityId == amenity!.AmenityId).ToListAsync();

            var slots = await _context.AmenityTimeSlots.Where(s => units.Select(u => u.UnitId).Contains(s.UnitId)).ToListAsync();

            var bookings = await _context.Bookings.Where(b => b.AmenityId == amenity!.AmenityId && b.BookingDate == date.Date).ToListAsync();

            var bookedSlots = bookings
                .GroupBy(b => b.Unit.UnitName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => new BookedSlotDto
                    {
                        Unit = x.Unit.UnitName,
                        Time = $"{DateTime.Today.Add(x.Slot.StartTime):h:mm tt} - " +
                               $"{DateTime.Today.Add(x.Slot.EndTime):h:mm tt}"
                        //Time = $"{x.Slot.StartTime} - {x.Slot.EndTime}"
                    }).ToList()
                );

            var unitDtos = units.Select(u => new AmenityUnitDto
            {
                UnitId = u.UnitId,
                Name = u.UnitName,
                Slots = slots
                    .Where(s => s.UnitId == u.UnitId)
                    .Select(s => new TimeSlotDto
                    {
                        SlotId = s.SlotId,
                        //Time = $"{s.StartTime} - {s.EndTime}",
                        Time = $"{DateTime.Today.Add(s.StartTime):h:mm tt} - " +
                               $"{DateTime.Today.Add(s.EndTime):h:mm tt}",
                        Available = !bookings.Any(b =>
                            b.Unit.UnitName == u.UnitName &&
                            b.Slot.StartTime == s.StartTime &&
                            b.Slot.EndTime == s.EndTime)
                    })
                    .ToList()
            }).ToList();

            return new AmenityBookingConfigDto
            {
                AmenityId = amenity!.AmenityId,
                Title = amenity!.Name,
                //UnitsLabel = "PIT",
                Units = unitDtos,
                BookedSlots = bookedSlots
            };
        }
    }
}
