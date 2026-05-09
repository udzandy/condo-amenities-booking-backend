using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Domain.Enums;
using CondoAmenitiesBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Repositories
{
    public class BookingRepository: IBookingService
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> HasConflict(int amenityId, DateTime start, DateTime end)
        //{
        //    return await _context.Bookings.AnyAsync(b =>
        //        b.AmenityId == amenityId &&
        //        b.Status == BookingStatus.Confirmed &&
        //        !(b.Slot.EndTime <= start || b.Slot.StartTime >= end));
        //}

        public async Task<bool> HasConflict(int unitId, int slotId, DateTime bookingDate)
        {
            return await _context.Bookings.AnyAsync(b =>
                b.UnitId == unitId &&
                b.SlotId == slotId &&
                b.BookingDate.Date == bookingDate.Date &&
                b.Status == BookingStatus.Confirmed
            );
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<List<BookingDto>> GetUserBookings(string userId)
        {
            //return await _context.Bookings
            //    .Include(x => x.Amenity)
            //    .Where(x => x.UserId == userId)
            //    .OrderByDescending(b => b.CreatedAt)
            //    .Select(x => new BookingDto
            //    {
            //        BookingId = x.BookingId,
            //        AmenityName = x.Amenity.Name,
            //        UnitName = x.Unit.UnitName,
            //        BookingDate = x.BookingDate,
            //        TimeSlot = $"{x.Slot.StartTime:hh:mm tt} - {x.Slot.EndTime:hh:mm tt}",
            //        Status = x.Status.ToString()
            //    })
            //    .ToListAsync();

            var bookings = await _context.Bookings
                .Include(x => x.Amenity)
                    .ThenInclude(x => x.Policy)
                .Include(x => x.Unit)
                .Include(x => x.Slot)
                .Where(x =>
                    x.UserId == userId)
                .OrderByDescending(x => x.BookingDate)
                .ToListAsync();

            var result = bookings.Select(x =>
            {
                // Calculate booking start datetime
                var bookingStart =
                    x.BookingDate.Date +
                    x.Slot.StartTime;

                // Hours remaining
                var hoursLeft =
                    (bookingStart - DateTime.UtcNow)
                    .TotalHours;

                // Policy hours
                var cancellationHours =
                    x.Amenity.Policy.CancellationHours;

                // Final flag
                var canCancel =
                    x.Status != BookingStatus.Cancelled &&
                    hoursLeft >= cancellationHours;

                return new BookingDto
                {
                    BookingId = x.BookingId,

                    AmenityName = x.Amenity.Name,

                    UnitName = x.Unit.UnitName,

                    BookingDate = x.BookingDate,

                    TimeSlot = $"{DateTime.Today.Add(x.Slot.StartTime):hh:mm tt} - " +
                               $"{DateTime.Today.Add(x.Slot.EndTime):hh:mm tt}",

                    Status = x.Status.ToString(),

                    CanCancel = canCancel
                };

            }).ToList();

            return result;
        }

        public async Task<(Booking?, bool)> CancelBooking(int bookingId, string userId)
        {
            var booking = await _context.Bookings
                                .Include(x => x.Amenity)
                                .ThenInclude(x => x.Policy)
                                .FirstOrDefaultAsync(x => x.BookingId == bookingId && x.UserId == userId);

            if (booking == null || booking.Status == BookingStatus.Cancelled)
                return (null, false);

            // GET POLICY HOURS
            var cancellationHours = booking.Amenity.Policy.CancellationHours;

            // HOURS LEFT
            var hoursLeft = (booking.BookingDate - DateTime.UtcNow).TotalHours;

            // RULE CHECK
            if (hoursLeft < cancellationHours)
                return (null, false);

            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return (booking, true);
        }
    }
}
