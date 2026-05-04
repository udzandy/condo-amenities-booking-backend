using CondoAmenitiesBooking.Application.Interfaces;

namespace CondoAmenitiesBooking.Infrastructure.Services
{
    public class EmailService: IEmailService
    {
        public Task SendAsync(string subject, string message)
        {
            Console.WriteLine($"EMAIL: {subject} - {message}");
            return Task.CompletedTask;
        }
    }
}
