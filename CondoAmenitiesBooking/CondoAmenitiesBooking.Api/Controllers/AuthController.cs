using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace CondoAmenitiesBooking.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest("Passwords do not match.");

            if (await _repository.ExistsByEmailAsync(dto.Email))
                return BadRequest("Email already registered.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Mobile = dto.Mobile,
                PasswordHash = Hash(dto.Password),
                EstateName = dto.EstateName,
                Block = dto.Block,
                UnitNumber = dto.UnitNumber,
                PostalCode = dto.PostalCode,
                OccupancyType = dto.OccupancyType
            };

            await _repository.AddAsync(user);

            return Ok("Registration successful");
        }

        private static string Hash(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
