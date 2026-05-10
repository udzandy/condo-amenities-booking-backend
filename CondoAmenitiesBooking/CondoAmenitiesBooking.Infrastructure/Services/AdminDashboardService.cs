using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Enums;
using CondoAmenitiesBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly AppDbContext _context;

        public AdminDashboardService(AppDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // SUMMARY
        // ==========================================

        public async Task<DashboardSummaryDto> GetSummary()
        {
            return new DashboardSummaryDto
            {
                TotalBookings =
                    await _context.Bookings.CountAsync(),

                ActiveUsers =
                    await _context.Users
                        .CountAsync(x => x.IsActive),

                TotalAmenities =
                    await _context.Amenities
                        .CountAsync(x => x.IsActive),

                TodayBookings =
                    await _context.Bookings
                        .CountAsync(x =>
                            x.BookingDate.Date == DateTime.Today),

                CancelledBookings =
                    await _context.Bookings
                        .CountAsync(x =>
                            x.Status == BookingStatus.Cancelled)
            };
        }

        // ==========================================
        // RECENT BOOKINGS
        // ==========================================

        public async Task<List<RecentBookingDto>>
            GetRecentBookings()
        {
            return await _context.Bookings
                .Include(x => x.User)
                .Include(x => x.Amenity)
                .Include(x => x.Unit)
                .Include(x => x.Slot)
                .OrderByDescending(x => x.CreatedAt)
                .Take(10)
                .Select(x => new RecentBookingDto
                {
                    BookingId = x.BookingId,

                    UserName =
                        x.User.FirstName + " " +
                        x.User.LastName,

                    AmenityName =
                        x.Amenity.Name,

                    UnitName =
                        x.Unit.UnitName,

                    BookingDate =
                        x.BookingDate,

                    TimeSlot =
                        DateTime.Today
                            .Add(x.Slot.StartTime)
                            .ToString("hh:mm tt")
                        + " - " +
                        DateTime.Today
                            .Add(x.Slot.EndTime)
                            .ToString("hh:mm tt"),

                    Status =
                        x.Status.ToString()
                })
                .ToListAsync();
        }

        // ==========================================
        // AUDIT LOGS
        // ==========================================

        public async Task<List<AuditLogDto>>
            GetAuditLogs()
        {
            return await _context.AuditLogs
                .OrderByDescending(x => x.Timestamp)
                .Take(20)
                .Select(x => new AuditLogDto
                {
                    AuditId = x.LogId,

                    Action = x.Action,

                    EntityName = x.Entity,

                    Details = x.Details,

                    CreatedAt = x.Timestamp
                })
                .ToListAsync();
        }
    }
}
