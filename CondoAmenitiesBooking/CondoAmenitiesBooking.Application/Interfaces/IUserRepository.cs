using CondoAmenitiesBooking.Domain.Entities;

namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
