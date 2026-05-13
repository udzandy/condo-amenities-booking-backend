using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        //private readonly IUserService _service;

        private readonly JwtService _jwtService;
        private readonly IUserService _userService;

        //public UserController(IUserService service)
        //{
        //    _service = service;
        //}

        public UserController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userService.ValidateUser(dto);

            //if (user == null)
            //    return Unauthorized();

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            }

            var token = _jwtService.GenerateToken(user.UserId, user.Role.ToString());

            //return Ok(new { token });

            return Ok(new
            {
                token,
                userId = user.UserId,
                name = $"{user.FirstName} {user.LastName}",
                role = user.Role.ToString()
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var userId = await _userService.Register(dto);
            return Ok(new { UserId = userId });
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginDto dto)
        //{
        //    var (userId, name) = await _service.Login(dto);

        //    if (userId == null)
        //        return Unauthorized("Invalid credentials");

        //    return Ok(new { UserId = userId, Name = name });
        //}

        [HttpGet("getActiveUsers")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllActiveUsers();
            return Ok(users);
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> Delete(string userId)
        {
            var success = await _userService.DeleteUser(userId);

            if (!success)
                return NotFound();

            return Ok("User deactivated");
        }
    }
}
