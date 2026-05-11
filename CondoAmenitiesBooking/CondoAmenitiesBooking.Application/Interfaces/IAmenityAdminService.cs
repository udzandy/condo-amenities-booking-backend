using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IAmenityAdminService
    {
        Task<List<AmenityDto>> GetAmenities();
        Task<AmenityDto> CreateAmenity(AmenityDto amenity);
        Task<AmenityDto?> UpdateAmenity(int id, AmenityDto amenity);
        Task DeleteAmenity(int amenityId);
        Task<List<AmenityUnitDto>> GetUnits();
        Task<AmenityUnitDto> CreateUnit(AmenityUnitDto unit);
        Task<AmenityUnitDto?> UpdateUnit(int id, AmenityUnitDto unit);
        Task DeleteUnit(int unitId);
        Task<List<AmenitySlotDto>> GetSlots();
        Task<AmenitySlotDto> CreateSlot(AmenitySlotDto slot);
        Task<AmenitySlotDto?> UpdateSlot(int id, AmenitySlotDto slot);
        Task DeleteSlot(int slotId);
    }
}
