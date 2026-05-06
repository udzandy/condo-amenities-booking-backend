using CondoAmenitiesBooking.Application.Features.Bookings.Handlers;
using CondoAmenitiesBooking.Application.Interfaces;
using CondoAmenitiesBooking.Infrastructure.Repositories;
using CondoAmenitiesBooking.Infrastructure.Services;

namespace CondoAmenitiesBooking.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserRepository>();
            services.AddScoped<IBookingService, BookingRepository>();
            services.AddScoped<IAmenityService, AmenityRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<GetUserBookingsHandler>();
            services.AddScoped<CancelBookingHandler>();
            
            return services;
        }
    }
}
