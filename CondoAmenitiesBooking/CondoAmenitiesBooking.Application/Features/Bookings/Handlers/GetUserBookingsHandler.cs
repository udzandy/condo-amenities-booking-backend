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

        public async Task<List<BookingDto>> Handle(string userId)
        {
            return await _service.GetUserBookings(userId);
        }
    }
}
