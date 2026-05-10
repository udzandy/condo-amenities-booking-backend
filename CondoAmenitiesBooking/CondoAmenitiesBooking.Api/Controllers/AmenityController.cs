using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Features.Amenities.Handlers;
using CondoAmenitiesBooking.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/amenities")]
    public class AmenityController : ControllerBase
    {
        private readonly GetAmenitiesHandler _getAmenitiesHandler;
        private readonly GetAmenityAvailabilityHandler _availabilityHandler;
        private readonly GetAmenitiesBookingConfigHandler _bookingConfigHandler;

        public AmenityController(
            GetAmenitiesHandler getAmenitiesHandler,
            GetAmenityAvailabilityHandler availabilityHandler,
            GetAmenitiesBookingConfigHandler bookingConfigHandler)
        {
            _getAmenitiesHandler = getAmenitiesHandler;
            _availabilityHandler = availabilityHandler;
            _bookingConfigHandler = bookingConfigHandler;
        }

        [HttpGet("getAmenities")]
        public async Task<IActionResult> GetAmenities()
        {
            var result = await _getAmenitiesHandler.GetAmenities();

            return Ok(result);
        }

        //[HttpGet("{amenityId}/availability")]
        [HttpGet("{slug}/availability")]
        //public async Task<IActionResult> GetAvailability(int amenityId, [FromQuery] DateTime bookingDate)
        public async Task<IActionResult> GetAvailability(string slug, [FromQuery] DateTime bookingDate)
        {
            var result = await _getAmenitiesHandler.GetAvailability(slug, bookingDate);

            return Ok(result);
        }

        [HttpGet("{slug}/booking-config")]
        public async Task<IActionResult> GetBookingConfig(string slug, [FromQuery] DateTime date)
        {
            var result = await _getAmenitiesHandler.GetBookingConfig(slug, date);
            return Ok(result);
        }
    }
}
