using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;

namespace CondoAmenitiesBooking.Application.Features.Amenities.Handlers
{
    public class GetAmenitiesHandler
    {
        private readonly IAmenityService _amenityService;

        public GetAmenitiesHandler(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        public async Task<List<AmenityDto>> GetAmenities()
        {
            return await _amenityService.GetAmenities();
        }

        public async Task<AmenityBookingConfigDto> GetBookingConfig(string slug, DateTime date)
        {
            return await _amenityService.GetBookingConfig(slug, date);
        }

        //public async Task<List<AmenityAvailabilityDto>> GetAvailability(int amenityId, DateTime bookingDate)
        public async Task<List<AmenityAvailabilityDto>> GetAvailability(string slug, DateTime bookingDate)
        {
            return await _amenityService.GetAvailability(slug, bookingDate);
        }
    }
}
