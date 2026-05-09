using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IBookingService
    {
        Task<bool> HasConflict(int unitId, int slotId, DateTime bookingDate);
        Task<Booking> CreateBooking(Booking booking);
        Task<List<BookingDto>> GetUserBookings(string userId);
        Task<(Booking?, bool)> CancelBooking(int bookingId, string userId);
    }
}
