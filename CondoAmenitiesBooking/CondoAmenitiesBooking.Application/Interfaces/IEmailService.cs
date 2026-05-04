namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string subject, string message);
    }
}
