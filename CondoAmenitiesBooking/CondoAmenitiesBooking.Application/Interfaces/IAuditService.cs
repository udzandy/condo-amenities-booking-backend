using CondoAmenitiesBooking.Application.DTOs;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IAuditService
    {
        Task LogAsync(string userId, string action, string entity, string details);
        Task<List<AuditLogDto>> SearchAsync(AuditLogFilterDto filter);
    }
}
