using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Services
{
    public class AuditService : IAuditService
    {
        private readonly AppDbContext _context;

        public AuditService(AppDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string userId, string action, string entity, string details)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Entity = entity,
                Details = details,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLogDto>> SearchAsync(AuditLogFilterDto filter)
        {
            var query = _context.AuditLogs
        .Include(a => a.User)
        .AsQueryable();

            // Case-insensitive filters
            if (!string.IsNullOrEmpty(filter.UserId))
                query = query.Where(a => a.UserId.ToLower() == filter.UserId.ToLower());

            if (!string.IsNullOrEmpty(filter.Action))
                query = query.Where(a => a.Action.ToLower() == filter.Action.ToLower());

            if (!string.IsNullOrEmpty(filter.Entity))
                query = query.Where(a => a.Entity.ToLower() == filter.Entity.ToLower());

            // Date filter
            if (filter.FromDate.HasValue)
                query = query.Where(a => a.Timestamp >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(a => a.Timestamp <= filter.ToDate.Value);

            // Partial search (Details + User Name)
            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                var search = filter.SearchText.ToLower();

                query = query.Where(a =>
                    a.Details!.ToLower().Contains(search) ||
                    (a.User.FirstName + " " + a.User.LastName).ToLower().Contains(search));
            }

            // Sorting
            query = query.OrderByDescending(a => a.Timestamp);

            // Pagination
            var skip = (filter.PageNumber - 1) * filter.PageSize;

            var result = await query
                .Skip(skip)
                .Take(filter.PageSize)
                .Select(a => new AuditLogDto
                {
                    LogId = a.LogId,
                    UserName = a.User.FirstName + " " + a.User.LastName,
                    Action = a.Action,
                    Entity = a.Entity,
                    Details = a.Details,
                    Timestamp = a.Timestamp
                })
                .ToListAsync();

            return result;
        }

    }
}
