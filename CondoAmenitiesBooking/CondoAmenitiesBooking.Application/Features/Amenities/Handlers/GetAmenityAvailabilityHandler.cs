using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;

namespace CondoAmenitiesBooking.Application.Features.Amenities.Handlers
{
    public class GetAmenityAvailabilityHandler
    {
        private readonly IAmenityService _amenityService;

        public GetAmenityAvailabilityHandler(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        //public async Task<List<AmenityAvailabilityDto>> GetAvailability(int amenityId, DateTime bookingDate)
        //{
        //    return await _amenityService.GetAvailability(amenityId, bookingDate);
        //}
    }
}
