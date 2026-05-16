using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;

namespace CondoAmenitiesBooking.Application.Features.Bookings.Handlers
{
    public class GetUserBookingsHandler
    {
        private readonly IBookingService _service;

        public GetUserBookingsHandler(IBookingService service)
        {
            _service = service;
        }

        public async Task<List<BookingDto>> GetUserBookings(string userId)
        {
            return await _service.GetUserBookings(userId);
        }

        public async Task<List<BookingDto>> GetAllBookings()
        {
            return await _service.GetAllBookings();
        }
    }
}
