using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IAmenityService
    {
        Task<Amenity?> GetById(int amenityId);
        Task<AmenityUnit?> GetUnitById(int unitId);
        Task<AmenityTimeSlot?> GetSlotById(int slotId);
    }
}
