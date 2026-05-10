using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;

namespace CondoAmenitiesBooking.Application.Features.AdminDashboard.Handlers
{
    public class GetAuditLogsHandler
    {
        private readonly IAdminDashboardService _service;

        public GetAuditLogsHandler(
            IAdminDashboardService service)
        {
            _service = service;
        }

        public async Task<List<AuditLogDto>> Handle()
        {
            return await _service.GetAuditLogs();
        }
    }
}
