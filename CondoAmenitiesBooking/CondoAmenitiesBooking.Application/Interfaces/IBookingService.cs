using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IBookingService
    {
        Task<bool> HasConflict(int amenityId, DateTime start, DateTime end);
        Task<Booking> CreateBooking(Booking booking);
    }
}
