using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Infrastructure.Email;
using Microsoft.Extensions.Options;
//using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CondoAmenitiesBooking.Infrastructure.Services
{
    public class EmailService: IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public Task SendAsync(string subject, string message)
        {
            Console.WriteLine($"EMAIL: {subject} - {message}");
            return Task.CompletedTask;
        }

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));

            email.To.Add(MailboxAddress.Parse(toEmail));

            email.Subject = subject;

            email.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(_settings.SmtpServer,_settings.Port,SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_settings.Username,_settings.Password);

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
    }
}
