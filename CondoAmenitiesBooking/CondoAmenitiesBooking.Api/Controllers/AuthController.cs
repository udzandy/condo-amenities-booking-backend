using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using CondoAmenitiesBooking.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasherService _hasher;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository repository, IPasswordHasherService hasher, IConfiguration config)
        {
            _repository = repository;
            _hasher = hasher;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _repository.GetByEmailAsync(dto.Email);

            if (user == null || !_hasher.VerifyPassword(user.PasswordHash, dto.Password))
            {
                var errorResponse = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid email or password",
                    Data = null,
                    StatusCode = 401
                };
                return Unauthorized(errorResponse);
            }

            var token = GenerateJwtToken(user);

            var response = new ApiResponse<LoginResponseDto>
            {
                Success = true,
                Message = "Login successful",
                Data = new LoginResponseDto
                {
                    Token = token,
                    Role = user.Role.ToString(),
                },
                StatusCode = 200
            };

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                var errorResponse = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Passwords do not match",
                    Data = null,
                    StatusCode = 400
                };
                return BadRequest(errorResponse);
            }

            if (await _repository.ExistsByEmailAsync(dto.Email))
            {
                var errorResponse = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Email already registered",
                    Data = null,
                    StatusCode = 400
                };
                return BadRequest(errorResponse);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Mobile = dto.Mobile,
                PasswordHash = _hasher.HashPassword(dto.Password),
                EstateName = dto.EstateName,
                Block = dto.Block,
                UnitNumber = dto.UnitNumber,
                PostalCode = dto.PostalCode,
                OccupancyType = dto.OccupancyType,
                Role = dto.Role
            };

            await _repository.AddAsync(user);

            var response = new ApiResponse<object>
            {
                Success = true,
                Message = "Registration successful",
                Data = new { user.Id, user.Email, user.Role },
                StatusCode = 201
            };

            return StatusCode(201, response);
        }

        private string GenerateJwtToken(User user)
        {
            var secret = _config["JwtSettings:Secret"];
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
