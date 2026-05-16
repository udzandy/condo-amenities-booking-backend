using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/admin/payment")]
    public class AdminPaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public AdminPaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // =========================
        // GET PAYMENTS
        // =========================

        [HttpGet("getPayments")]
        public async Task<IActionResult> GetAdminPayments()
        {
            var result = await _paymentService.GetAdminPayments();

            return Ok(result);
        }

        // =====================================================
        // PAY BOOKING
        // =====================================================

        [HttpPost("pay")]
        public async Task<IActionResult> PayBooking([FromBody] PayBookingRequestDto request)
        {
            var result = await _paymentService.PayBooking(request);

            if (!result.IsSuccess)
                return BadRequest(new
                {
                    success = false,
                    message = result.Error
                });

            return Ok(new
            {
                success = true,
                message = "Payment Successfull"
            });
        }
    }
}
