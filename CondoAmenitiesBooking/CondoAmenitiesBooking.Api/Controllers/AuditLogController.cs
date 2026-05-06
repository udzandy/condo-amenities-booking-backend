using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/auditlogs")]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditService _service;

        public AuditLogController(IAuditService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AuditLogFilterDto filter)
        {
            var result = await _service.SearchAsync(filter);
            return Ok(result);
        }
    }
}
