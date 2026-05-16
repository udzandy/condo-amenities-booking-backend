using CondoAmenitiesBooking.Application.DTOs;
using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> Register(RegisterUserDto dto);
        Task<(string?, string?)> Login(LoginDto dto);
        Task<List<UserDto>> GetAllActiveUsers();
        Task<bool> DeleteUser(string userId);
        Task<bool> RejectUser(string userId);
        Task<bool> ApprovedUser(string userId);
        Task<User?> GetById(string userId);
        Task<(User?, string?)> ValidateUser(LoginDto dto);
    }
}
