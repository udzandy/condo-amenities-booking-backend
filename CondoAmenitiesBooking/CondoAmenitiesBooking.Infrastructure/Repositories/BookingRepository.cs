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
    }
}
