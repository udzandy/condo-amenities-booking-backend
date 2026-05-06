using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var userId = await _service.Register(dto);
            return Ok(new { UserId = userId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var (userId, name) = await _service.Login(dto);

            if (userId == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { UserId = userId, Name = name });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllActiveUsers();
            return Ok(users);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var success = await _service.DeleteUser(userId);

            if (!success)
                return NotFound();

            return Ok("User deactivated");
        }
    }
}
