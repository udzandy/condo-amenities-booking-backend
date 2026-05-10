using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;

namespace CondoAmenitiesBooking.Application.Features.AdminDashboard.Handlers
{
    public class GetDashboardSummaryHandler
    {
        private readonly IAdminDashboardService _service;

        public GetDashboardSummaryHandler(
            IAdminDashboardService service)
        {
            _service = service;
        }

        public async Task<DashboardSummaryDto> Handle()
        {
            return await _service.GetSummary();
        }
    }
}
