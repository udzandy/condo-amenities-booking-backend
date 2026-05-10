using CondoAmenitiesBooking.Application.DTOs;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IAdminDashboardService
    {
        Task<DashboardSummaryDto> GetSummary();
        Task<List<RecentBookingDto>> GetRecentBookings();
        Task<List<AuditLogDto>> GetAuditLogs();
    }
}
