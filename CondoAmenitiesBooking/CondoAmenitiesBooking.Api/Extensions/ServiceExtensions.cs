using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Infrastructure.Repositories;
using CondoAmenitiesBooking.Infrastructure.Services;

namespace CondoAmenitiesBooking.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBookingService, BookingRepository>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
