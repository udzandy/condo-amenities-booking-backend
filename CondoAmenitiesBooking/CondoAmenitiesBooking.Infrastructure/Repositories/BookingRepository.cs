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

        public async Task<bool> HasConflict(int amenityId, DateTime start, DateTime end)
        {
            return await _context.Bookings.AnyAsync(b =>
                b.AmenityId == amenityId &&
                b.Status == BookingStatus.Confirmed &&
                !(b.EndTime <= start || b.StartTime >= end));
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
            //    .Include(b => b.Amenity)
            //    .Where(b => b.UserId == userId)
            //    .OrderByDescending(b => b.StartTime)
            //    .Select(b => new BookingDto
            //    {
            //        BookingId = b.BookingId,
            //        AmenityName = b.Amenity.Name,
            //        Date = b.StartTime.Date,
            //        TimeRange = $"{b.StartTime:hh:mm tt} - {b.EndTime:hh:mm tt}",
            //        Status = b.Status.ToString()
            //    })
            //    .ToListAsync();

            return await _context.Bookings
                .Include(x => x.Amenity)
                .Where(x => x.UserId == userId)
                .OrderByDescending(b => b.StartTime)
                .Select(x => new BookingDto
                {
                    BookingId = x.BookingId,
                    Amenity = x.Amenity.Name,
                    //Unit = x.UnitName,
                    Date = x.StartTime.ToString("yyyy-MM-dd"),
                    Time = $"{x.StartTime:hh:mm tt} - {x.EndTime:hh:mm tt}",
                    Status = x.Status.ToString()
                })
                .ToListAsync();
        }

        public async Task<Booking?> CancelBooking(int bookingId, string userId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Amenity)
                .ThenInclude(a => a.Rules)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId && b.UserId == userId);

            if (booking == null || booking.Status == BookingStatus.Cancelled)
                return null;

            var rule = booking.Amenity.Rules.FirstOrDefault();

            // Default: allow cancel anytime
            if (rule != null)
            {
                // Example: "Cancel 24 hours before"
                if (rule.CancellationPolicy.Contains("hours"))
                {
                    var hours = int.Parse(rule.CancellationPolicy.Split(' ')[1]);

                    if (DateTime.UtcNow > booking.StartTime.AddHours(-hours))
                        return null; // too late to cancel
                }
            }

            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return booking;
        }
    }
}
