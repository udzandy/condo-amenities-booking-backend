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

        public BookingController(CreateBookingHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingCommand command)
        {
            var result = await _handler.Handle(command);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(new { BookingId = result.Value });
        }
    }
}
