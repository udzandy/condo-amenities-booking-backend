using CondoAmenitiesBooking.Application.DTOs;
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

        public AdminAmenitiesController(IAmenityAdminService service)
        {
            _service = service;
        }

        // =========================
        // GET AMENITIES
        // =========================

        [HttpGet("getAmenities")]
        public async Task<IActionResult> GetAmenities()
        {
            var result = await _service.GetAmenities();

            return Ok(result);
        }

        // =========================
        // CREATE AMENITY
        // =========================

        [HttpPost("createAmenity")]
        public async Task<IActionResult> CreateAmenity([FromBody] AmenityDto amenity)
        {
            var result = await _service.CreateAmenity(amenity);

            return Ok(result);
        }

        // =========================
        // UPDATE AMENITY
        // =========================

        [HttpPut("updateAmenity/{id}")]
        public async Task<IActionResult> UpdateAmenity(int id, [FromBody] AmenityDto amenity)
        {
            await _service.UpdateAmenity(id, amenity);

            return Ok();
        }

        // =========================
        // DELETE AMENITY
        // =========================

        [HttpDelete("deleteAmenity/{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            await _service.DeleteAmenity(id);

            return Ok();
        }

        // =========================
        // GET UNITS
        // =========================

        [HttpGet("getUnits")]
        public async Task<IActionResult> GetUnits()
        {
            var result = await _service.GetUnits();

            return Ok(result);
        }

        // =========================
        // CREATE UNIT
        // =========================

        [HttpPost("createUnit")]
        public async Task<IActionResult> CreateUnit([FromBody] AmenityUnitDto unit)
        {
            var result = await _service.CreateUnit(unit);

            return Ok(result);
        }

        // =========================
        // UPDATE UNIT
        // =========================

        [HttpPut("updateUnit/{id}")]
        public async Task<IActionResult> UpdateUnit(int id, [FromBody] AmenityUnitDto unit)
        {
            await _service.UpdateUnit(id, unit);

            return Ok();
        }

        // =========================
        // DELETE UNIT
        // =========================

        [HttpDelete("deleteUnit/{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            await _service.DeleteUnit(id);

            return Ok();
        }

        // =========================
        // GET SLOTS
        // =========================

        [HttpGet("getSlots")]
        public async Task<IActionResult> GetSlots()
        {
            var result = await _service.GetSlots();

            return Ok(result);
        }

        // =========================
        // CREATE SLOT
        // =========================

        [HttpPost("createSlot")]
        public async Task<IActionResult> CreateSlot([FromBody] AmenitySlotDto slot)
        {
            var result = await _service.CreateSlot(slot);

            return Ok(result);
        }

        // =========================
        // UPDATE SLOT
        // =========================

        [HttpPut("updateSlot/{id}")]
        public async Task<IActionResult> UpdateSlot(int id, [FromBody] AmenitySlotDto slot)
        {
            await _service.UpdateSlot(id, slot);

            return Ok();
        }

        // =========================
        // DELETE SLOT
        // =========================

        [HttpDelete("deleteSlot/{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            await _service.DeleteSlot(id);

            return Ok();
        }
    }
}
