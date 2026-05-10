using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/admin/amenities")]
    public class AdminAmenitiesController : ControllerBase
    {
        private readonly IAmenityAdminService _service;

        public AdminAmenitiesController(
            IAmenityAdminService service)
        {
            _service = service;
        }

        // =========================
        // GET ALL
        // =========================

        [HttpGet]
        public async Task<IActionResult> GetAmenities()
        {
            var result = await _service.GetAmenities();

            return Ok(result);
        }

        // =========================
        // CREATE AMENITY
        // =========================

        [HttpPost]
        public async Task<IActionResult> CreateAmenity(
            [FromBody] Amenity amenity)
        {
            var result =
                await _service.CreateAmenity(amenity);

            return Ok(result);
        }

        // =========================
        // UPDATE AMENITY
        // =========================

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAmenity(
            int id,
            [FromBody] Amenity amenity)
        {
            amenity.AmenityId = id;

            await _service.UpdateAmenity(amenity);

            return Ok();
        }

        // =========================
        // DELETE AMENITY
        // =========================

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            await _service.DeleteAmenity(id);

            return Ok();
        }

        // =========================
        // CREATE UNIT
        // =========================

        [HttpPost("units")]
        public async Task<IActionResult> CreateUnit(
            [FromBody] AmenityUnit unit)
        {
            var result =
                await _service.CreateUnit(unit);

            return Ok(result);
        }

        // =========================
        // UPDATE UNIT
        // =========================

        [HttpPut("units/{id}")]
        public async Task<IActionResult> UpdateUnit(
            int id,
            [FromBody] AmenityUnit unit)
        {
            unit.UnitId = id;

            await _service.UpdateUnit(unit);

            return Ok();
        }

        // =========================
        // DELETE UNIT
        // =========================

        [HttpDelete("units/{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            await _service.DeleteUnit(id);

            return Ok();
        }

        // =========================
        // CREATE SLOT
        // =========================

        [HttpPost("slots")]
        public async Task<IActionResult> CreateSlot(
            [FromBody] AmenityTimeSlot slot)
        {
            var result =
                await _service.CreateSlot(slot);

            return Ok(result);
        }

        // =========================
        // UPDATE SLOT
        // =========================

        [HttpPut("slots/{id}")]
        public async Task<IActionResult> UpdateSlot(
            int id,
            [FromBody] AmenityTimeSlot slot)
        {
            slot.SlotId = id;

            await _service.UpdateSlot(slot);

            return Ok();
        }

        // =========================
        // DELETE SLOT
        // =========================

        [HttpDelete("slots/{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            await _service.DeleteSlot(id);

            return Ok();
        }
    }
}
