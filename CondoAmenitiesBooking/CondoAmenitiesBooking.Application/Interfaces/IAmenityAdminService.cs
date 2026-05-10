using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IAmenityAdminService
    {
        Task<List<Amenity>> GetAmenities();
        Task<Amenity> CreateAmenity(Amenity amenity);
        Task UpdateAmenity(Amenity amenity);
        Task DeleteAmenity(int amenityId);
        Task<AmenityUnit> CreateUnit(AmenityUnit unit);
        Task UpdateUnit(AmenityUnit unit);
        Task DeleteUnit(int unitId);
        Task<AmenityTimeSlot> CreateSlot(AmenityTimeSlot slot);
        Task UpdateSlot(AmenityTimeSlot slot);
        Task DeleteSlot(int slotId);
    }
}
