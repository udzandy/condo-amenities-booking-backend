namespace CondoAmenitiesBooking.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string subject, string message);
        Task SendAsync(string toEmail, string subject, string body);
    }
}
