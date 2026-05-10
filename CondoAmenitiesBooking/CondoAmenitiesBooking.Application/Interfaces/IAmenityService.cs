using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IAmenityService
    {
        Task<Amenity?> GetById(int amenityId);
        Task<AmenityUnit?> GetUnitById(int unitId);
        Task<AmenityTimeSlot?> GetSlotById(int slotId);
        Task<List<AmenityDto>> GetAmenities();
        Task<List<AmenityAvailabilityDto>> GetAvailability(string slug, DateTime bookingDate);
        Task<AmenityBookingConfigDto> GetBookingConfig(string slug, DateTime date);
    }
}
