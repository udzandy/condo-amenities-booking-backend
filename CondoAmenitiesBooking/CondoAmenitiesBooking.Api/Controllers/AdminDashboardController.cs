using CondoAmenitiesBooking.Application.Features.AdminDashboard.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/admin-dashboard")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly GetDashboardSummaryHandler _summaryHandler;
        private readonly GetRecentBookingsHandler _recentBookingsHandler;
        private readonly GetAuditLogsHandler _auditLogsHandler;

        public AdminDashboardController(
            GetDashboardSummaryHandler summaryHandler,
            GetRecentBookingsHandler recentBookingsHandler,
            GetAuditLogsHandler auditLogsHandler)
        {
            _summaryHandler = summaryHandler;
            _recentBookingsHandler = recentBookingsHandler;
            _auditLogsHandler = auditLogsHandler;
        }

        // ==========================================
        // SUMMARY
        // ==========================================

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _summaryHandler.Handle();

            return Ok(result);
        }

        // ==========================================
        // RECENT BOOKINGS
        // ==========================================

        [HttpGet("recent-bookings")]
        public async Task<IActionResult> GetRecentBookings()
        {
            var result = await _recentBookingsHandler.Handle();

            return Ok(result);
        }

        // ==========================================
        // AUDIT LOGS
        // ==========================================

        [HttpGet("audit-logs")]
        public async Task<IActionResult> GetAuditLogs()
        {
            var result = await _auditLogsHandler.Handle();

            return Ok(result);
        }
    }
}
