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
        Task<AddAmenityUnitDto> CreateUnit(AddAmenityUnitDto unit);
        Task<UpdateAmenityUnitDto?> UpdateUnit(int id, UpdateAmenityUnitDto unit);
        Task DeleteUnit(int unitId);
        Task<List<AmenitySlotDto>> GetSlots();
        Task<AddAmenitySlotDto> CreateSlot(AddAmenitySlotDto slot);
        Task<UpdateAmenitySlotDto?> UpdateSlot(int id, UpdateAmenitySlotDto slot);
        Task DeleteSlot(int slotId);
    }
}
