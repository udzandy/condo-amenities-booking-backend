using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;

namespace CondoAmenitiesBooking.Application.Features.Amenities.Handlers
{
    public class GetAmenitiesBookingConfigHandler
    {
        private readonly IAmenityService _amenityService;

        public GetAmenitiesBookingConfigHandler(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        public async Task<AmenityBookingConfigDto> Handle(string slug, DateTime date)
        {
            return await _amenityService.GetBookingConfig(slug, date);
        }
    }
}
