using CondoAmenitiesBooking.Application.Features.Bookings.Commands;
using CondoAmenitiesBooking.Application.Features.Bookings.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController: ControllerBase
    {
        private readonly CreateBookingHandler _handler;
        private readonly GetUserBookingsHandler _getHandler;
        private readonly CancelBookingHandler _cancelHandler;

        public BookingController(CreateBookingHandler handler, 
                                 GetUserBookingsHandler getHandler,
                                 CancelBookingHandler cancelHandler)
        {
            _handler = handler;
            _getHandler = getHandler;
            _cancelHandler = cancelHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingCommand command)
        {
            var result = await _handler.Handle(command);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(new { BookingId = result.Value });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBookings(string userId)
        {
            var result = await _getHandler.Handle(userId);
            return Ok(result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] CancelBookingCommand command)
        {
            var result = await _cancelHandler.Handle(command);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok("Booking cancelled successfully");
        }
    }
}
