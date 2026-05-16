using CondoAmenitiesBooking.Application.Features.AdminDashboard.Handlers;
using CondoAmenitiesBooking.Application.Features.Amenities.Handlers;
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
            services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAdminDashboardService, AdminDashboardService>();
            services.AddScoped<IAmenityAdminService,AmenityAdminService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<GetUserBookingsHandler>();
            services.AddScoped<CancelBookingHandler>();
            services.AddScoped<GetAmenitiesHandler>();
            services.AddScoped<GetAmenityAvailabilityHandler>();
            services.AddScoped<GetAmenitiesBookingConfigHandler>();
            services.AddScoped<GetDashboardSummaryHandler>();
            services.AddScoped<GetRecentBookingsHandler>();
            services.AddScoped<GetAuditLogsHandler>();

            return services;
        }
    }
}
