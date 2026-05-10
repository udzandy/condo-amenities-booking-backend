using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;

namespace CondoAmenitiesBooking.Application.Features.AdminDashboard.Handlers
{
    public class GetRecentBookingsHandler
    {
        private readonly IAdminDashboardService _service;

        public GetRecentBookingsHandler(
            IAdminDashboardService service)
        {
            _service = service;
        }

        public async Task<List<RecentBookingDto>> Handle()
        {
            return await _service.GetRecentBookings();
        }
    }
}
